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
            var assetLocator = new FromDirectoryAssetLocator(httpContext.Server, httpContext.Request.PhysicalApplicationPath);
            var builderContext = contextFactory.CreateStyleSheetContext();
            var bundleProvider = new StyleSheetBundleProvider(DefaultSettings.StyleSheetConfigProvider, new BundlesCache<StyleSheetBundle>(cacheProvider), assetLocator, builderContext);            

            var urlGenerator = new StyleSheetUrlGenerator();
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetBundleCollectionResolver(resolverFactory);            
            var merger = new StyleSheetWebAssetMerger(
                new WebAssetReader(httpContext.Server),
                new ImagePathContentFilter(),
                DefaultSettings.StyleSheetCompressor,
                httpContext.Server,
                new MergedBundleCache(WebAssetType.StyleSheet, cacheProvider));            
            var tagWriter = new StyleSheetTagWriter(urlGenerator);

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(bundleProvider.GetBundles()),
                collectionResolver,
                tagWriter,
                merger,                
                builderContext);
        }

        public ScriptManagerBuilder CreateScriptManagerBuilder()
        {
            var assetLocator = new FromDirectoryAssetLocator(httpContext.Server, httpContext.Request.PhysicalApplicationPath);
            var builderContext = contextFactory.CreateScriptContext();
            var bundleProvider = new ScriptBundleProvider(DefaultSettings.ScriptConfigProvider, new BundlesCache<ScriptBundle>(cacheProvider), assetLocator, builderContext);

            var urlGenerator = new ScriptUrlGenerator();
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetBundleCollectionResolver(resolverFactory);
            var merger = new ScriptWebAssetMerger(
                new WebAssetReader(httpContext.Server),
                DefaultSettings.ScriptCompressor, 
                new MergedBundleCache(WebAssetType.Script, cacheProvider));            
            var tagWriter = new ScriptTagWriter(urlGenerator);

            return new ScriptManagerBuilder(
                new ScriptManager(bundleProvider.GetBundles()),
                collectionResolver,
                tagWriter,
                merger,                
                builderContext);
        }
    }
}
