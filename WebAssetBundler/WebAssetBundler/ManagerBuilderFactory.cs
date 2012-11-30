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
    using System.Web.Mvc;
    using System.Web;

    public class ManagerBuilderFactory
    {
        private HttpContextBase httpContext;
        private ICacheProvider cacheProvider;
        private IBuilderContextFactory contextFactory;

        public ManagerBuilderFactory(HttpContextBase httpContext, ICacheProvider cacheProvider, IBuilderContextFactory contextFactory)
        {
            this.httpContext = httpContext;
            this.cacheProvider = cacheProvider;
            this.contextFactory = contextFactory;
        }

        public StyleSheetManagerBuilder CreateStyleSheetManagerBuilder()
        {
            var container = WabHttpModule.Host.Container;
            var assetLocator = new FromDirectoryAssetLocator(httpContext.Server, httpContext.Request.PhysicalApplicationPath);
            var builderContext = contextFactory.CreateStyleSheetContext();
            var bundleProvider = new StyleSheetBundleProvider(
                DefaultSettings.StyleSheetConfigProvider, 
                container.Resolve<IBundlesCache<StyleSheetBundle>>(),
                container.Resolve<IAssetLocator<FromDirectoryComponent>>(), 
                builderContext);            

            var urlGenerator = new StyleSheetUrlGenerator();
            var merger = container.Resolve<StyleSheetWebAssetMerger>();    
            var tagWriter = new StyleSheetTagWriter(urlGenerator);

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(bundleProvider.GetBundles()),
                container.Resolve<IWebAssetBundleCollectionResolver>(),
                tagWriter,
                merger,                
                builderContext);
        }

        public ScriptManagerBuilder CreateScriptManagerBuilder()
        {
            var container = WabHttpModule.Host.Container;
            var builderContext = contextFactory.CreateScriptContext();
            var bundleProvider = new ScriptBundleProvider(
                DefaultSettings.ScriptConfigProvider, 
                container.Resolve<IBundlesCache<ScriptBundle>>(), 
                container.Resolve<IAssetLocator<FromDirectoryComponent>>(), 
                builderContext);

            var urlGenerator = new ScriptUrlGenerator();
            var merger = container.Resolve<ScriptWebAssetMerger>();         
            var tagWriter = new ScriptTagWriter(urlGenerator);

            return new ScriptManagerBuilder(
                new ScriptManager(bundleProvider.GetBundles()),
                container.Resolve<IWebAssetBundleCollectionResolver>(),
                tagWriter,
                merger,                
                builderContext);
        }
    }
}
