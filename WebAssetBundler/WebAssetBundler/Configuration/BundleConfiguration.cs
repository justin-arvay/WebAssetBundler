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
        /// <param name="source"></param>
        public void Add(string source)
        {
            bundle.Assets.Add(new WebAsset(source));
        }

        /// <summary>
        /// Adds all files in a directory of the correct file type.
        /// </summary>
        /// <param name="path"></param>
        public void AddFromDirectory(string path)
        {
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

            var assets = AssetLocator.Locate(component);

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
        /// Informs the bundling process to combine all assets into one bundle.
        /// </summary>
        /// <param name="combine"></param>
        public void Combine(bool combine)
        {
            bundle.Combine = combine;
        }

        /// <summary>
        /// Informs the bundling process to compress all assets when bundling.
        /// </summary>
        /// <param name="compress"></param>
        public void Compress(bool compress)
        {
            bundle.Compress = compress;
        }

        /// <summary>
        /// Sets a specific host for the bundle.s
        /// </summary>
        /// <param name="host"></param>
        public void Host(string host)
        {
            bundle.Host = host;
        }

        public TBundle GetBundle()
        {
            return bundle;
        }

        internal IAssetLocator<FromDirectoryComponent> AssetLocator
        {
            get;
            set;
        }

        public abstract void Configure();
    }
}
