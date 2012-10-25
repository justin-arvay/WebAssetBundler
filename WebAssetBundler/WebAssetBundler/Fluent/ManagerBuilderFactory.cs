﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

    public class ManagerBuilderFactory
    {
        private ViewContext viewContext;
        private ICacheProvider cacheProvider;
        private SharedGroupManager sharedManager;
        private IBuilderContextFactory contextFactory;

        public ManagerBuilderFactory(ViewContext viewContext, ICacheProvider cacheProvider, IBuilderContextFactory contextFactory, SharedGroupManager sharedManager)
        {
            this.viewContext = viewContext;
            this.cacheProvider = cacheProvider;
            this.sharedManager = sharedManager;
            this.contextFactory = contextFactory;
        }

        public StyleSheetManagerBuilder CreateStyleSheetManagerBuilder()
        {
            var builderContext = contextFactory.CreateStyleSheetContext();

            var urlGenerator = new StyleSheetUrlGenerator();
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);            
            var merger = new StyleSheetWebAssetMerger(
                new WebAssetReader(viewContext.HttpContext.Server),
                new ImagePathContentFilter(),
                DefaultSettings.StyleSheetCompressor,
                viewContext.HttpContext.Server,
                new MergedResultCache(WebAssetType.StyleSheet, cacheProvider));            
            var tagWriter = new StyleSheetTagWriter(urlGenerator);

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(new WebAssetGroupCollection()),
                sharedManager.StyleSheets,
                viewContext,
                collectionResolver,
                tagWriter,
                merger,                
                builderContext);
        }

        public ScriptManagerBuilder CreateScriptManagerBuilder()
        {
            var builderContext = contextFactory.CreateScriptContext();                 

            var urlGenerator = new ScriptUrlGenerator();
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var merger = new ScriptWebAssetMerger(
                new WebAssetReader(viewContext.HttpContext.Server),
                DefaultSettings.ScriptCompressor, 
                new MergedResultCache(WebAssetType.Script, cacheProvider));            
            var tagWriter = new ScriptTagWriter(urlGenerator);

            return new ScriptManagerBuilder(
                new ScriptManager(new WebAssetGroupCollection()),
                sharedManager.Scripts,
                viewContext,
                collectionResolver,
                tagWriter,
                merger,                
                builderContext);
        }
    }
}
