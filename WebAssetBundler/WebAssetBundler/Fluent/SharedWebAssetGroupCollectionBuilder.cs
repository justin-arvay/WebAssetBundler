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

    public class SharedWebAssetGroupCollectionBuilder
    {
        private WebAssetGroupCollection groups;
        private BuilderContext context;

        public SharedWebAssetGroupCollectionBuilder(WebAssetGroupCollection groups, BuilderContext context)
        {
            this.groups = groups;
            this.context = context;

        }

        public SharedWebAssetGroupCollectionBuilder AddGroup(string name, Action<WebAssetGroupBuilder> action)
        {
            //ensure that we cannot add the same group twice
            if (groups.FindGroupByName(name) != null)
            {
                throw new ArgumentException(TextResource.Exceptions.GroupWithSpecifiedNameAlreadyExists);
            }

            var group = context.AssetFactory.CreateGroup(name, false);

            //add to collection
            groups.Add(group);

            //call action
            action(new WebAssetGroupBuilder(group, groups, context));
            return this;
        }
    }
}
