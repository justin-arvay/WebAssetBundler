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

    public class BundleMetadataCache : IBundleMetadataCache
    {
        private ICacheProvider provider;

        public BundleMetadataCache(ICacheProvider provider)
        {
            this.provider = provider;
        }

        public BundleMetadata GetMetadata<TBundle>(string name)
            where TBundle : Bundle
        {
            string key = GetKey(name, typeof(TBundle));
            return (BundleMetadata)provider.Get(key);
        }

        public void AddMetadata(BundleMetadata metadata)
        {
            string key = GetKey(metadata.Name, metadata.Type);
            provider.Insert(key, metadata);
        }

        private string GetKey(string name, Type type)
        {
            return name + "-" + type.Name;
        }
    }
}
