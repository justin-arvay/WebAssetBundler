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

namespace WebAssetBundler.Web.Mvc
{
    using System;

    public class WebAssetGroupCollectionBuilder
    {        
        private WebAssetGroupCollection groups;
        private WebAssetGroupCollection sharedGroups;
        private string generatedPath;

        public WebAssetGroupCollectionBuilder(WebAssetGroupCollection groups, WebAssetGroupCollection sharedGroups, string generatedPath)
        {            
            this.groups = groups;
            this.generatedPath = generatedPath;
            this.sharedGroups = sharedGroups;
        }


        public WebAssetGroupCollectionBuilder AddGroup(string name, Action<WebAssetGroupBuilder> configureAction)
        {
            //ensure that we cannot add the same group twice
            if (groups.FindGroupByName(name) != null) 
            {
                throw new ArgumentException(TextResource.Exceptions.GroupWithSpecifiedNameAlreadyExists);
            }

            var group = new WebAssetGroup(name, false, generatedPath);

            //add to collection
            groups.Add(group);

            //call action
            configureAction(new WebAssetGroupBuilder(group));
            return this;
        }

        /// <summary>
        /// Adds a file to the collection.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public WebAssetGroupCollectionBuilder Add(string source)
        {
            var group = new WebAssetGroup("Single", false, generatedPath) ;

            group.Assets.Add(new WebAsset(source));

            //add to collection
            groups.Add(group);

            return this;
        }

        /// <summary>
        /// Adds a shared group.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public WebAssetGroupCollectionBuilder AddSharedGroup(string name)
        {
            var group = FindSharedGroup(name);
            groups.Add(group);
            return this;
        }

        /// <summary>
        /// Adds a shared group and allows to override configuration.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configureAction">Allows overriding of shared configuration if needed.</param>
        /// <returns></returns>
        public WebAssetGroupCollectionBuilder AddSharedGroup(string name, Action<WebAssetGroupBuilder> configureAction)
        {
            var group = FindSharedGroup(name);

            groups.Add(group);
            configureAction(new WebAssetGroupBuilder(group));
            
            return this;
        }

        private WebAssetGroup FindSharedGroup(string name)
        {
            var group = sharedGroups.FindGroupByName(name);

            if (group == null)
            {
                throw new ArgumentException("Shared group does not exist: " + name);
            }

            return group;
        }
    }
}
