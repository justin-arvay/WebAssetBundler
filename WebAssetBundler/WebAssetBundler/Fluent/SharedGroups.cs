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
    using System.Collections.Generic;

    public static class SharedGroups
    {
        private static BuilderContext scriptContext;
        private static BuilderContext styleSheetContext;

        static SharedGroups()
        {
            LoadManager();
        }

        public static SharedGroupManager Manager
        {
            get;
            private set;
        }

        public static BuilderContext ScriptContext
        {
            get
            {
                scriptContext = new BuilderContext(WebAssetType.Script);
                scriptContext.AssetFactory = new AssetFactory(scriptContext);

                return scriptContext;
            }            
        }

        public static BuilderContext StyleSheetContext
        {
            get
            {
                styleSheetContext = new BuilderContext(WebAssetType.StyleSheet);
                styleSheetContext.AssetFactory = new AssetFactory(styleSheetContext);

                return styleSheetContext;
            }            
        }

        public static void StyleSheets(Action<SharedWebAssetGroupCollectionBuilder> action)
        {
            action(new SharedWebAssetGroupCollectionBuilder(Manager.StyleSheets, StyleSheetContext));
        }

        public static void Scripts(Action<SharedWebAssetGroupCollectionBuilder> action)
        {
            action(new SharedWebAssetGroupCollectionBuilder(Manager.Scripts, ScriptContext));
        }

        private static void LoadManager()
        {
            Manager = new SharedGroupManager();
            var loader = new SharedGroupManagerLoader(new AssetConfigurationFactory());

            loader.Load(Manager);
        }
    }
}