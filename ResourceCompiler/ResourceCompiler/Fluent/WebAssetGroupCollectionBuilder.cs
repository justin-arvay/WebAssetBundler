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

    public class WebAssetGroupCollectionBuilder
    {

        private WebAssetType type;

        private WebAssetGroupCollection resourceGroups;

        public WebAssetGroupCollectionBuilder(WebAssetType type, WebAssetGroupCollection resourceGroups)
        {
            this.type = type;
            this.resourceGroups = resourceGroups;
        }


        public WebAssetGroupCollectionBuilder AddGroup(string name, Action<WebAssetGroupBuilder> configureAction)
        {
            //ensure that we cannot add the same group twice
            if (resourceGroups.FindGroupByName(name) != null) 
            {
                throw new ArgumentException(TextResource.Exceptions.GroupWithSpecifiedNameAlreadyExists);
            }

            var group = new WebAssetGroup(name, false) ;

            //add to collection
            resourceGroups.Add(group);

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
            var group = new WebAssetGroup("Single", false) ;

            group.Assets.Add(new WebAsset(source));

            //add to collection
            resourceGroups.Add(group);

            return this;
        }

        /// <summary>
        /// Adds a new group and allows you to configure that group, or add an existing group as a shared group and configure it differently for this use.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public WebAssetGroupCollectionBuilder AddSharedGroup(string name, Action<WebAssetGroupBuilder> configureAction)
        {
            throw new NotImplementedException();
            return this;
        }
    }
}
