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

    public class WebHost : IDisposable
    {
        private TinyIoCContainer container;

        public WebHost()
        {
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
            container.Register<IBundlePipeline<StyleSheetBundle>>((c, p) => new StyleSheetPipeline(container));
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
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => new ScriptPipeline(container));
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

        public void ConfigureHttpHandler()
        {
            container.Register<HttpHandlerFactory>()
                .AsSingleton();
            container.Register<EncoderFactory>()
                .AsSingleton();
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
