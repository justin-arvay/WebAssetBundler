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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class WebAssetGroupBuilder
    {
        private readonly WebAssetGroup group;
        private readonly WebAssetGroupCollection sharedGroups;

        public WebAssetGroupBuilder(WebAssetGroup group, WebAssetGroupCollection sharedGroups)
        {
            this.group = group;
            this.sharedGroups = sharedGroups;
        }

        public WebAssetGroupBuilder Enable(bool value)
        {
            group.Enabled = value;
            return this;
        }

        public WebAssetGroupBuilder Version(string value)
        {
            group.Version = value;
            return this;
        }

        public WebAssetGroupBuilder Compress(bool value)
        {
            group.Compress = value;
            return this;
        }

        public WebAssetGroupBuilder Combine(bool value)
        {
            group.Combine = value;
            return this;
        }

        /// <summary>
        /// Adds a new asset to the group by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Add(string filePath)
        {
            group.Assets.Add(new WebAsset(filePath));
            return this;
        }

        /// <summary>
        /// Adds all assets from the shared group. Uses the current groups settings and not those from the shared group.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder AddShared(string name)
        {
            var sharedGroup = sharedGroups.FindGroupByName(name);

            if (sharedGroup.IsIncluded)
            {
                throw new InvalidOperationException(TextResource.Exceptions.CannotAddSharedGroupTwice);
            }

            foreach (var asset in sharedGroup.Assets)
            {
                group.Assets.Add(asset);
            }

            sharedGroup.IsIncluded = true;

            return this;            
        }
    }
}
