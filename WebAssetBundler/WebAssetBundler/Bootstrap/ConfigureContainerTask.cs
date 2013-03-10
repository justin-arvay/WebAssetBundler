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
    using System.Web;

    public class ConfigureContainerTask : IBootstrapTask
    {

        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            ConfigureCommon(container, typeProvider);
            ConfigureContainerForScript(container, typeProvider);
            ConfigureContainerForStyleSheets(container, typeProvider);
            ConfigureHttpHandler(container);

        }

        public void ConfigureHttpHandler(TinyIoCContainer container)
        {
            container.Register<HttpHandlerFactory>()
                .AsSingleton();
            container.Register<EncoderFactory>()
                .AsSingleton();
        }

        public void ConfigureCommon(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            container.Register((c, p) => HttpContext());
            container.Register<HttpServerUtilityBase>(HttpContext().Server);
            container.Register((c, p) => HttpContext().Request);
            container.Register((c, p) => HttpContext().Response);
            container.Register((c, p) => HttpContext().Server);
            container.Register<ICacheProvider, CacheProvider>();
            container.Register<IDirectoryFactory, DirectoryFactory>();

            container.Register<IAssetProvider>((c, p) => new AssetProvider(
                c.Resolve<IDirectoryFactory>(),
                c.Resolve<HttpServerUtilityBase>(),
                () => DefaultSettings.MinifyIdentifier,
                () => DefaultSettings.DebugMode));

            container.Register<ITypeProvider>(typeProvider);
            container.Register<WabSettings>((c, p) => CreateSettings());
            
        }

        public void ConfigureContainerForStyleSheets(TinyIoCContainer container, ITypeProvider typeProvider)
        {            
            container.Register<IStyleSheetMinifier>((c, p) => DefaultSettings.StyleSheetMinifier);
            container.Register<IBundlesCache<StyleSheetBundle>, BundlesCache<StyleSheetBundle>>();
            container.Register<IBundleConfigurationProvider<StyleSheetBundle>>((c, p) => DefaultSettings.StyleSheetConfigurationProvider(c));
            container.Register<IBundlePipeline<StyleSheetBundle>>((c, p) => CreateStyleSheetPipeline(c, typeProvider));
            container.Register<ITagWriter<StyleSheetBundle>, StyleSheetTagWriter>();
            container.Register<IBundleProvider<StyleSheetBundle>, StyleSheetBundleProvider>();
            container.Register<IBundleCachePrimer<StyleSheetBundle>, StyleSheetBundleCachePrimer>();
            container.Register<IBundleProvider<StyleSheetBundle>, StyleSheetBundleProvider>();
        }

        public void ConfigureContainerForScript(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            container.Register<IScriptMinifier>((c, p) => DefaultSettings.ScriptMinifier);
            container.Register<IBundlesCache<ScriptBundle>, BundlesCache<ScriptBundle>>();
            container.Register<IBundleConfigurationProvider<ScriptBundle>>((c, p) => DefaultSettings.ScriptConfigurationProvider(c));
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => CreateScriptPipeline(c, typeProvider));            
            container.Register<ITagWriter<ScriptBundle>, ScriptTagWriter>();
            container.Register<IBundleCachePrimer<ScriptBundle>, ScriptBundleCachePrimer>();
            container.Register<IBundleProvider<ScriptBundle>, ScriptBundleProvider>();
        }

        public IBundlePipeline<ScriptBundle> CreateScriptPipeline(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var pipeline = new ScriptPipeline(container);

            container.RegisterMultiple<IPipelineCustomizer<ScriptBundle>>(typeProvider.GetImplementationTypes(typeof(IPipelineCustomizer<ScriptBundle>)));

            foreach (var customizer in container.ResolveAll<IPipelineCustomizer<ScriptBundle>>())
            {
                customizer.Customize(pipeline);
            }

            return pipeline;
        }

        public IBundlePipeline<StyleSheetBundle> CreateStyleSheetPipeline(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var pipeline = new StyleSheetPipeline(container);

            container.RegisterMultiple<IPipelineCustomizer<StyleSheetBundle>>(typeProvider.GetImplementationTypes(typeof(IPipelineCustomizer<StyleSheetBundle>)));

            foreach (var customizer in container.ResolveAll<IPipelineCustomizer<StyleSheetBundle>>())
            {
                customizer.Customize(pipeline);
            }

            return pipeline;
        }

        private HttpContextBase HttpContext()
        {
            return new HttpContextWrapper(System.Web.HttpContext.Current);
        }

        private WabSettings CreateSettings()
        {
            return new WabSettings()
            {
                MinifyIdentifier = DefaultSettings.MinifyIdentifier,
                DebugMode = DefaultSettings.DebugMode
            };
        }
    }
}
