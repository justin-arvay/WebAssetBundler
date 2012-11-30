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

    public class MergedBundleCache<T> : IMergedBundleCache<T>
    {
        private ICacheProvider provider;
        private const string keyPrefix = "MergedResult";

        public MergedBundleCache(ICacheProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Adds the bundle to the cache.
        /// </summary>
        /// <param name="bundle"></param>
        public void Add(MergedBundle result)
        {
            provider.Insert(GetKey(result.Name), result);
        }

        public MergedBundle Get(string name)
        {
            return (MergedBundle)provider.Get(GetKey(name));
        }

        /// <summary>
        /// Creates the key for the given bundle.
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        private string GetKey(string name)
        {
            var type = typeof(T);

            return keyPrefix + "->" + type.Name + "->" + name;
        }
    }
}
