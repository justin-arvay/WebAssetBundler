// Web Asset Bundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Compilation;

    public class WebHost : IDisposable
    {
        private TinyIoCContainer container;
        private Type[] allTypes;
        private TypeProvider typeProvider;


        public WebHost()
        {
            typeProvider = new TypeProvider(LoadAssemblies());
            allTypes = typeProvider.GetAllTypes();
            container = new TinyIoCContainer();
        }

        public TinyIoCContainer Container
        {
            get
            {
                return container;
            }
        }

        public void ConfigureContainer()
        {
            container.Register((c, p) => HttpContext());
            container.Register<HttpServerUtilityBase>(HttpContext().Server);
            container.Register((c, p) => HttpContext().Request);
            container.Register((c, p) => HttpContext().Response);
            container.Register((c, p) => HttpContext().Server);
            container.Register<ICacheProvider, CacheProvider>();

            container.Register<IAssetProvider>((c, p) => new AssetProvider(
                c.Resolve<HttpServerUtilityBase>(), 
                HttpContext().Request.PhysicalApplicationPath,
                () => DefaultSettings.MinifyIdentifier,
                () => DefaultSettings.DebugMode));

            ConfigureContainerForStyleSheets();
            ConfigureContainerForScript();
            ConfigureHttpHandler();
        }

        public void ConfigureContainerForStyleSheets()
        {
            container.Register<IUrlGenerator<StyleSheetBundle>>(new StyleSheetUrlGenerator(() => DefaultSettings.DebugMode));
            container.Register<IStyleSheetMinifier>((c, p) => DefaultSettings.StyleSheetMinifier);
            container.Register<IBundlesCache<StyleSheetBundle>, BundlesCache<StyleSheetBundle>>();
            container.Register<IConfigProvider<StyleSheetBundleConfiguration>>((c, p) => DefaultSettings.StyleSheetConfigProvider);
            container.Register<IBundlePipeline<StyleSheetBundle>>((c, p) => CreateStyleSheetPipeline(c));
            container.Register<ITagWriter<StyleSheetBundle>, StyleSheetTagWriter>();
            container.Register<IBundleProvider<StyleSheetBundle>, StyleSheetBundleProvider>();
            container.Register<IBundleCachePrimer<StyleSheetBundle, StyleSheetBundleConfiguration>, StyleSheetBundleCachePrimer>();

            container.Register<StyleSheetMinifyProcessor>((c, p) => new StyleSheetMinifyProcessor(
                () => DefaultSettings.MinifyIdentifier,
                () => DefaultSettings.DebugMode,
                container.Resolve<IStyleSheetMinifier>()));

            container.Register<IBundleProvider<StyleSheetBundle>>((c, p) => new StyleSheetBundleProvider(
                container.Resolve<IConfigProvider<StyleSheetBundleConfiguration>>(),
                container.Resolve<IBundlesCache<StyleSheetBundle>>(),
                container.Resolve<IBundlePipeline<StyleSheetBundle>>(),
                container.Resolve<IAssetProvider>(),
                container.Resolve<IBundleCachePrimer<StyleSheetBundle, StyleSheetBundleConfiguration>>(),
                () => DefaultSettings.DebugMode));
        }

        public void ConfigureContainerForScript()
        {
            container.Register<IScriptMinifier>((c, p) => DefaultSettings.ScriptMinifier);
            container.Register<IBundlesCache<ScriptBundle>, BundlesCache<ScriptBundle>>();
            container.Register<IConfigProvider<ScriptBundleConfiguration>>((c, p) => DefaultSettings.ScriptConfigProvider);
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => CreateScriptPipeline(c));
            container.Register<IUrlGenerator<ScriptBundle>>(new ScriptUrlGenerator(() => DefaultSettings.DebugMode));
            container.Register<ITagWriter<ScriptBundle>, ScriptTagWriter>();
            container.Register<IBundleCachePrimer<ScriptBundle, ScriptBundleConfiguration>, ScriptBundleCachePrimer>();

            container.Register<ScriptMinifyProcessor>((c, p) => new ScriptMinifyProcessor(
                () => DefaultSettings.MinifyIdentifier,
                () => DefaultSettings.DebugMode,
                container.Resolve<IScriptMinifier>()));

            container.Register<IBundleProvider<ScriptBundle>>((c, p) => new ScriptBundleProvider(
                container.Resolve<IConfigProvider<ScriptBundleConfiguration>>(),
                container.Resolve<IBundlesCache<ScriptBundle>>(),
                container.Resolve<IAssetProvider>(),
                container.Resolve<IBundlePipeline<ScriptBundle>>(),
                container.Resolve<IBundleCachePrimer<ScriptBundle, ScriptBundleConfiguration>>(), 
                () => DefaultSettings.DebugMode));
        }

        public IBundlePipeline<ScriptBundle> CreateScriptPipeline(TinyIoCContainer container)
        {
            var pipeline = new ScriptPipeline(container);

            container.RegisterMultiple<IPipelineCustomizer<ScriptBundle>>(typeProvider.GetImplementationTypes(typeof(IPipelineCustomizer<ScriptBundle>)));

            foreach (var customizer in container.ResolveAll<IPipelineCustomizer<ScriptBundle>>())
            {
                customizer.Customize(pipeline);
            }

            return pipeline;
        }

        public IBundlePipeline<StyleSheetBundle> CreateStyleSheetPipeline(TinyIoCContainer container)
        {
            var pipeline = new StyleSheetPipeline(container);

            container.RegisterMultiple<IPipelineCustomizer<StyleSheetBundle>>(typeProvider.GetImplementationTypes(typeof(IPipelineCustomizer<StyleSheetBundle>)));

            foreach (var customizer in container.ResolveAll<IPipelineCustomizer<StyleSheetBundle>>())
            {
                customizer.Customize(pipeline);
            }

            return pipeline;
        }

        /*
        protected virtual IEnumerable<Type> GetConfigurationTypes(IEnumerable<Type> typesToSearch)
        {
            var publicTypes =
                from type in typesToSearch
                where type.IsClass && !type.IsAbstract
                from interfaceType in type.GetInterfaces()
                where interfaceType.IsGenericType &&
                      interfaceType.GetGenericTypeDefinition() == typeof(IConfiguration<>)
                select type;

            var internalTypes = new[]
            {
                typeof(ScriptContainerConfiguration),
                typeof(StylesheetsContainerConfiguration),
                typeof(HtmlTemplatesContainerConfiguration),
                typeof(SettingsVersionAssigner)
            };

            return publicTypes.Concat(internalTypes);
        }*/

        public void ConfigureHttpHandler()
        {
            container.Register<HttpHandlerFactory>()
                .AsSingleton();
            container.Register<EncoderFactory>()
                .AsSingleton();
        }

        private  IEnumerable<Assembly> LoadAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToArray();
            //return BuildManager.GetReferencedAssemblies().Cast<Assembly>();
        }

        private HttpContextBase HttpContext()
        {
            return new HttpContextWrapper(System.Web.HttpContext.Current);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
