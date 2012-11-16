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
            this.bundle = bundle;
        }

        public void Add(string source)
        {
            bundle.Assets.Add(new WebAsset(source));
        }

        public void Name(string name)
        {
            bundle.Name = name;
        }

        public void Combine(bool combine)
        {
            bundle.Combine = combine;
        }

        public void Compress(bool compress)
        {
            bundle.Compress = compress;
        }

        public void Host(string host)
        {
            bundle.Host = host;
        }

        public TBundle GetBundle()
        {
            return bundle;
        }

        public abstract void Configure();
    }
}
