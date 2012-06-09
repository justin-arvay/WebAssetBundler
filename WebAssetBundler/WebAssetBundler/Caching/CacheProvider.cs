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
    using System.Linq;
    using System.Web;
    using System.Web.Caching;

    public class CacheProvider : ICacheProvider
    {
        /// <summary>
        /// Retrieve an item from the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }
        
        /// <summary>
        /// Insert an item into the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(string key, object value)
        {
            if (key.IsNotNullOrEmpty() && value != null)
            {
                HttpRuntime.Cache.Insert(key, value);
            }
        }

        /// <summary>
        /// Insert an item into the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheItemRemovedCallback"></param>
        /// <param name="fileDependencies"></param>
        public void Insert(string key, object value, CacheItemRemovedCallback cacheItemRemovedCallback, params string[] fileDependencies)
        {
            if (key.IsNotNullOrEmpty() && value != null)
            {
                HttpRuntime.Cache.Insert(key, value, fileDependencies.Any() ? new CacheDependency(fileDependencies) : null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration,
                    CacheItemPriority.Normal, cacheItemRemovedCallback);
            }
        }
    }
}
