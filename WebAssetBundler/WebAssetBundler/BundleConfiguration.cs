﻿// WebAssetBundler - Compiles web assets so you dont have to.
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

    public abstract class BundleConfiguration<T, TBundle> : IConfigurable
        where T : BundleConfiguration<T, TBundle>
        where TBundle : Bundle
    {
        private TBundle bundle;

        public BundleConfiguration(TBundle bundle)
        {            
            bundle.Name = this.GetType().Name;
            this.bundle = bundle;
        }

        /// <summary>
        /// Add an asset to the bundle
        /// </summary>
        /// <param name="stream"></param>
        public void Add(string source)
        {
            bundle.Assets.Add(AssetProvider.GetAsset(source));
        }

        /// <summary>
        /// Adds all files in a directory of the correct file type.
        /// </summary>
        /// <param name="path"></param>
        public void AddFromDirectory(string path)
        {
            var assets = AssetProvider.GetAssets(new FromDirectoryComponent(path, bundle.Extension));

            foreach (var asset in assets)
            {
                bundle.Assets.Add(asset);
            }
        }

        /// <summary>
        /// Adds files from a directory based on rulesets. Only looks at files of the correct type.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="builder"></param>
        public void AddFromDirectory(string path, Action<FromDirectoryBuilder> builder)
        {
            var component = new FromDirectoryComponent(path, bundle.Extension);
            builder(new FromDirectoryBuilder(component));

            var assets = AssetProvider.GetAssets(component);

            foreach (var asset in assets)
            {
                bundle.Assets.Add(asset);
            }
        }

        /// <summary>
        /// Configure the bundle name. The bundle name is used in the url.
        /// </summary>
        /// <param name="name"></param>
        public void Name(string name)
        {
            bundle.Name = name;
        }

        /// <summary>
        /// Informs the bundling process to compress all assets when bundling.
        /// </summary>
        /// <param name="compress"></param>
        public void Compress(bool compress)
        {
            bundle.Minify = compress;
        }

        /// <summary>
        /// Sets a specific host for the bundle.s
        /// </summary>
        /// <param name="host"></param>
        public void Host(string host)
        {
            bundle.Host = host;
        }

        /// <summary>
        /// Time to live of the browser cache in minutes.
        /// </summary>
        /// <param name="timeToLive"></param>
        public void BrowserTtl(int timeToLive)
        {
            bundle.BrowserTtl = timeToLive;
        }

        public TBundle GetBundle()
        {
            return bundle;
        }

        internal IAssetProvider AssetProvider
        {
            get;
            set;
        }

        public abstract void Configure();
    }
}