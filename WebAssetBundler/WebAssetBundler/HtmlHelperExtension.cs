﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web;

    public static class HtmlHelperExtension
    {       

        public static ComponentBuilder Bundler(this HtmlHelper helper)
        {                                    
            var viewContext = helper.ViewContext;
            var cacheProvider = new CacheProvider();
             
            var sharedManager = (HttpContext.Current.Items["SharedManager"] ??
                (HttpContext.Current.Items["SharedManager"] = CreateSharedGroupManager())) as SharedGroupManager;

            var styleSheetManagerBuilder = (HttpContext.Current.Items["StyleSheetManagerBuilder"] ??
                        (HttpContext.Current.Items["StyleSheetManagerBuilder"] =
                        CreateStyleSheetManagerBuilder(viewContext, cacheProvider, sharedManager))) as StyleSheetManagerBuilder;

            var scriptManagerBuilder = (HttpContext.Current.Items["ScriptManagerBuilder"] ??
                        (HttpContext.Current.Items["ScriptManagerBuilder"] =
                        CreateScriptManagerBuilder(viewContext, cacheProvider, sharedManager))) as ScriptManagerBuilder;


            return new ComponentBuilder(
                styleSheetManagerBuilder,
                scriptManagerBuilder);
        }

        private static SharedGroupManager CreateSharedGroupManager()
        {
            var loader = new SharedGroupManagerLoader(new AssetConfigurationFactory());
            var sharedManager = new SharedGroupManager();

            loader.Load(sharedManager);

            return sharedManager;
        }

        private static StyleSheetManagerBuilder CreateStyleSheetManagerBuilder(ViewContext viewContext, ICacheProvider cacheProvider, SharedGroupManager sharedManager)
        {
            var pathResolver = new PathResolver(WebAssetType.StyleSheet);
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new StyleSheetWebAssetMerger(
                new WebAssetReader(viewContext.HttpContext.Server),
                new ImagePathContentFilter(),
                DefaultSettings.StyleSheetCompressor,
                pathResolver, 
                viewContext.HttpContext.Server);
            var generator = new WebAssetGenerator(writer, new MergedResultCache(WebAssetType.StyleSheet, cacheProvider));
            var tagWriter = new StyleSheetTagWriter(urlResolver);

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(new WebAssetGroupCollection()),
                sharedManager.StyleSheets,
                viewContext,
                collectionResolver,
                tagWriter,
                merger,
                generator);
        }

        private static ScriptManagerBuilder CreateScriptManagerBuilder(ViewContext viewContext, ICacheProvider cacheProvider, SharedGroupManager sharedManager)
        {

            var pathResolver = new PathResolver(WebAssetType.Script);
            
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new ScriptWebAssetMerger(new WebAssetReader(viewContext.HttpContext.Server), pathResolver, DefaultSettings.ScriptCompressor);
            var generator = new WebAssetGenerator(writer, new MergedResultCache(WebAssetType.Script, cacheProvider));
            var tagWriter = new ScriptTagWriter(urlResolver);

            return new ScriptManagerBuilder(
                new ScriptManager(new WebAssetGroupCollection()),
                sharedManager.Scripts,
                viewContext,
                collectionResolver,
                tagWriter,
                merger,
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