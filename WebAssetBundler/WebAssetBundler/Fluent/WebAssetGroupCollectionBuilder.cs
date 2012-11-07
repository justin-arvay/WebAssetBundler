// WebAssetBundler - Bundles web assets so you dont have to.
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

    public class WebAssetGroupCollectionBuilder
    {        
        private WebAssetGroupCollection groups;
        private WebAssetGroupCollection sharedGroups;
        private BuilderContext context;

        public WebAssetGroupCollectionBuilder(WebAssetGroupCollection groups, WebAssetGroupCollection sharedGroups, BuilderContext context)
        {            
            this.groups = groups;            
            this.sharedGroups = sharedGroups;
            this.context = context;
        }

        /// <summary>
        /// Adds a file to the collection.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public WebAssetGroupCollectionBuilder Add(string source)
        {
            var asset = context.AssetFactory.CreateAsset(source, "");
            var group = context.AssetFactory.CreateGroup(asset.Name, false);

            group.Assets.Add(asset);

            //add to collection
            groups.Add(group);

            return this;
        }A
    }
}