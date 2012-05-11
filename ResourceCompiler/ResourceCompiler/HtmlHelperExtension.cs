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



namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web;

    public static class HtmlHelperExtension
    {
        private static ISharedGroupManagerFactory sharedGroupManagerFactory;

        public static ComponentBuilder Reco(this HtmlHelper helper)
        {
            //
            if (sharedGroupManagerFactory == null)
            {
                //sharedGroupManagerFactory = new SharedGroupManagerFactory(new SharedGroupManagerLoader());
            }

            var viewContext = helper.ViewContext;
            var cacheProvider = new CacheProvider();

            var styleSheetManagerBuilder = (HttpContext.Current.Items["StyleSheetManagerBuilder"] ??
                        (HttpContext.Current.Items["StyleSheetManagerBuilder"] =
                        CreateStyleSheetManagerBuilder(viewContext, cacheProvider))) as StyleSheetManagerBuilder;

            var scriptManagerBuilder = (HttpContext.Current.Items["ScriptManagerBuilder"] ??
                        (HttpContext.Current.Items["ScriptManagerBuilder"] =
                        CreateScriptManagerBuilder(viewContext, cacheProvider))) as ScriptManagerBuilder;


            return new ComponentBuilder(
                styleSheetManagerBuilder,
                scriptManagerBuilder);
        }

        private static StyleSheetManagerBuilder CreateStyleSheetManagerBuilder(ViewContext viewContext, ICacheProvider cacheProvider)
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
                new StyleSheetManager(new WebAssetGroupCollection()),
                viewContext,
                collectionResolver,
                tagWriter,
                generator);
        }

        private static ScriptManagerBuilder CreateScriptManagerBuilder(ViewContext viewContext, ICacheProvider cacheProvider)
        {

            var pathResolver = new PathResolver(WebAssetType.Script);
            
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new ScriptWebAssetMerger(new WebAssetReader(viewContext.HttpContext.Server), DefaultSettings.ScriptCompressor);
            var generator = new WebAssetGenerator(writer, merger, new MergedResultCache(WebAssetType.Script, cacheProvider));
            var tagWriter = new ScriptTagWriter(urlResolver);

            return new ScriptManagerBuilder(
                new ScriptManager(new WebAssetGroupCollection()),
                viewContext,
                collectionResolver,
                tagWriter,
                generator);
        }

        /*
        public class SingletonPerRequest
        {
            public static SingletonPerRequest Current
            {
                get
                {
                    return (HttpContext.Current.Items["SingletonPerRequest"] ??
                        (HttpContext.Current.Items["SingletonPerRequest"] =
                        new SingletonPerRequest())) as SingletonPerRequest;

                }
            }
        }*/
    }
}
