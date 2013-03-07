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

    public abstract class BundleConfiguration<TBundle> : IBundleConfiguration<TBundle>
        where TBundle : Bundle
    {

        /// <summary>
        /// Add an assets to the bundle
        /// </summary>
        /// <param name="stream"></param>
        public void Add(string source)
        {
            Bundle.Assets.Add(AssetProvider.GetAsset(source));
        }

        /// <summary>
        /// Adds all files in a directory of the correct file type.
        /// </summary>
        /// <param name="path"></param>
        public void AddFromDirectory(string path)
        {
            var assets = AssetProvider.GetAssets(new DirectorySearchContext(path, Bundle.Extension));

            foreach (var asset in assets)
            {
                Bundle.Assets.Add(asset);
            }
        }

        /// <summary>
        /// Adds files from a directory based on rulesets. Only looks at files of the correct type.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="builder"></param>
        public void AddFromDirectory(string path, Action<DirectorySearchBuilder> builder)
        {
            var component = new DirectorySearchContext(path, Bundle.Extension);
            builder(new DirectorySearchBuilder(component));

            var assets = AssetProvider.GetAssets(component);

            foreach (var asset in assets)
            {
                Bundle.Assets.Add(asset);
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

        public abstract void Configure();
    }
}
