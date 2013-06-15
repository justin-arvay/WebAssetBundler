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

    public interface IFluentConfiguration<TBundle> 
        where TBundle : Bundle
    {
        void Add(string source);
        void AddDirectory(string path);
        void AddDirectory(string path, Action<DirectorySearchBuilder> builder);
        void AddDirectory(string path, DirectorySearch dirSearch);
        void Name(string name);
        void Minify(bool compress);
        void Host(string host);
        void BrowserTtl(int timeToLive);

        BundleMetadata Metadata { get; set; }
        IAssetProvider AssetProvider { get; set; }
        IDirectorySearchFactory DirectorySearchFactory { get; set; }

        /// <summary>
        /// Sets a bundle that is required for this bundle to function. When this bundle is rendered all bundles it requires 
        /// are rendered before it (recursively). You can only require bundles of the same bundle type.
        /// </summary>
        /// <param name="bundleName"></param>
        void Required(string bundleName);
        void Configure();
    }
}
