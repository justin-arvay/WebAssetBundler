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
        private BuilderContext context;

        public WebAssetGroupBuilder(WebAssetGroup group, WebAssetGroupCollection sharedGroups, BuilderContext context)
        {
            this.group = group;
            this.sharedGroups = sharedGroups;
            this.context = context;
        }
        
        /// <summary>
        /// Enables or disables the group. A disabled group does not get included on the page.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Enable(bool value)
        {
            group.Enabled = value;
            return this;
        }

        /// <summary>
        /// Sets the version for the group. Overrides all other versioning.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Version(string value)
        {
            group.Version = value;
            return this;
        }

        /// <summary>
        /// Compresses the group.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Compress(bool value)
        {
            group.Compress = value;
            return this;
        }

        /// <summary>
        /// Combines all the assets in the group into one file.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Combine(bool value)
        {
            group.Combine = value;
            return this;
        }

        /// <summary>
        /// Sets a path to use when adding assets to the group.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder DefaultPath(string path)
        {
            if (path.StartsWith("~/") == false)
            {
                throw new ArgumentException(string.Format(TextResource.Exceptions.PathMustBeVirtual, path));
            }

            group.DefaultPath = path;
            return this;
        }

        /// <summary>
        /// Adds a new asset to the group by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Add(string filePath)
        {
            group.Assets.Add(context.AssetFactory.CreateAsset(filePath, group.DefaultPath));
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

            foreach (var asset in sharedGroup.Assets)
            {
                group.Assets.Add(asset);
            }            

            return this;            
        }
    }
}
