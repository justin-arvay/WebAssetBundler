// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler
{
    using System.Web.Mvc;
    using ResourceCompiler.Web.Mvc;

    public class ComponentFactory
    {

        private ViewContext viewContext;

        private ICacheFactory cacheFactory;
        private ICacheProvider cacheProvider;

        public ComponentFactory(ViewContext viewContext, ICacheProvider cacheProvider)
        {
            this.viewContext = viewContext;
            this.cacheProvider = cacheProvider;
        }

        public StyleSheetManagerBuilder StyleSheetManager()
        {
            var pathResolver = new PathResolver(WebAssetType.StyleSheet);
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new StyleSheetWebAssetMerger(
                new WebAssetReader(viewContext.HttpContext.Server), 
                new ImagePathContentFilter(), 
                DefaultSettings.StyleSheetCompressor, 
                viewContext.HttpContext.Server);
            var generator = new WebAssetGenerator(writer, merger, new MergedResultCache(WebAssetType.StyleSheet, cacheProvider));
            var tagWriter = new StyleSheetTagWriter(urlResolver);

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(collection), 
                viewContext, 
                collectionResolver,
                tagWriter,                                
                cacheFactory,
                generator);
        }

        public ScriptManagerBuilder ScriptManager()
        {
            var pathResolver = new PathResolver(WebAssetType.Script);
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new ScriptWebAssetMerger(new WebAssetReader(viewContext.HttpContext.Server), DefaultSettings.ScriptCompressor);
            var generator = new WebAssetGenerator(writer, merger, new MergedResultCache(WebAssetType.Script, cacheProvider));
            var tagWriter = new ScriptTagWriter(urlResolver);

            return new ScriptManagerBuilder(
                new ScriptManager(collection),
                viewContext,
                collectionResolver,
                tagWriter,                
                cacheFactory,
                generator);
        }
    }
}
