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

    public class MergedResultCache : IMergedResultCache
    {
        private ICacheProvider provider;
        private const string keyPrefix = "MergedResult";
        private WebAssetType type;

        public MergedResultCache(WebAssetType type, ICacheProvider provider)
        {
            this.provider = provider;
            this.type = type;
        }

        /// <summary>
        /// Adds the result to the cache.
        /// </summary>
        /// <param name="result"></param>
        public void Add(MergerResult result)
        {
            provider.Insert(GetKey(result.Name), result);
        }

        public MergerResult Get(string name)
        {
            return (MergerResult)provider.Get(GetKey(name));
        }

        /// <summary>
        /// Creates the key for the given result.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string GetKey(string name)
        {
            var typePrefix = "";

            switch (type)
            {
                case WebAssetType.Script:
                    typePrefix = "Script";
                    break;
                case WebAssetType.StyleSheet:
                    typePrefix = "StyleSheet";
                    break;
            }

            return keyPrefix + "->" + typePrefix + "->" + name;
        }
    }
}
