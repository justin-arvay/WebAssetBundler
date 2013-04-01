// WebAssetBundler - Compiles web assets so you dont have to.
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
    using System.IO;
    using System.Linq;

    public abstract class BundleConfiguration<TBundle> : IBundleConfiguration<TBundle>
        where TBundle : Bundle
    {

        /// <summary>
        /// Add an assets to the bundle
        /// </summary>
        /// <param name="stream"></param>
        public void Add(string source)
        {
            var asset = AssetProvider.GetAsset(source);

            if (AlreadyExists(asset))
            {
                throw new ArgumentException(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists.FormatWith(asset.Source));
            }

            Bundle.Assets.Add(asset);
        }

        /// <summary>
        /// Adds all files in a directory based on the default search constraints.
        /// </summary>
        /// <param name="path"></param>
        public void AddDirectory(string path)
        {
            var assets = AssetProvider.GetAssets(path, DirectorySearchFactory.Create(Bundle.Extension));

            foreach (var asset in assets)
            {
                if (AlreadyExists(asset) == false)
                {
                    Bundle.Assets.Add(asset);
                }
            }
        }

        /// <summary>
        /// Adds all files in a directory based on the search constraints provided.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dirSearch"></param>
        public void AddDirectory(string path, DirectorySearch dirSearch)
        {
            var assets = AssetProvider.GetAssets(path, dirSearch);

            foreach (var asset in assets)
            {
                if (AlreadyExists(asset) == false)
                {
                    Bundle.Assets.Add(asset);
                }
            }
        }

        /// <summary>
        /// Adds files from a directory based on search constraints.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="builder"></param>
        public void AddDirectory(string path, Action<DirectorySearchBuilder> builder)
        {
            var component = DirectorySearchFactory.Create(Bundle.Extension);
            builder(new DirectorySearchBuilder((DirectorySearch)component));

            var assets = AssetProvider.GetAssets(path, component);

            foreach (var asset in assets)
            {
                if (AlreadyExists(asset) == false)
                {
                    Bundle.Assets.Add(asset);
                }
            }
        }



        /// <summary>
        /// Configure the bundle name. The bundle name is used in the url.
        /// </summary>
        /// <param name="name"></param>
        public void Name(string name)
        {
            Bundle.Name = name;
        }

        /// <summary>
        /// Informs the bundling process to compress all assets when bundling.
        /// </summary>
        /// <param name="compress"></param>
        public void Compress(bool compress)
        {
            Bundle.Minify = compress;
        }

        /// <summary>
        /// Sets a specific host for the bundle.s
        /// </summary>
        /// <param name="host"></param>
        public void Host(string host)
        {
            Bundle.Host = host;
        }

        /// <summary>
        /// Time to live of the browser cache in minutes.
        /// </summary>
        /// <param name="timeToLive"></param>
        public void BrowserTtl(int timeToLive)
        {
            Bundle.BrowserTtl = timeToLive;
        }

        public TBundle Bundle
        { 
            get; 
            set; 
        }

        public IAssetProvider AssetProvider
        {
            get;
            set;
        }

        public IDirectorySearchFactory<TBundle> DirectorySearchFactory
        {
            get;
            set;
        }

        public abstract void Configure();

        private bool AlreadyExists(AssetBase item)
        {
            return Bundle.Assets.Any(i => i.Source.Equals(item.Source));
        }

    }
}
