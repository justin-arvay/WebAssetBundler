// ResourceCompiler - Compiles web assets so you dont have to.
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
    
    public class Cache : ICache
    {
        private readonly ICacheProvider provider;
        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the Cache class.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="provider"></param>
        public Cache(string prefix, ICacheProvider provider)
        {
            this.provider = provider;
            this.prefix = prefix;
        }

        /// <summary>
        /// Allows retrieval of a cached item by key from the provider or insertion of the default item it not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValueFactory"></param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> defaultValueFactory)
        {
            var value = provider.Get(Prefix(key));

            //no cached value available
            if (value == null)
            {
                var defaultValue = defaultValueFactory();

                Insert(key, defaultValue);

                return defaultValue;
            }

            return (T)value;
        }
        
        /// <summary>
        /// Insert a new item into the cache provider.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert<T>(string key, T value)
        {
            provider.Insert(Prefix(key), value);
        }
        

        public string Prefix(string key)
        {
            return prefix + "->" + key;
        }
        
        /// <summary>
        /// Try and get cached item by key. True if successful.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue<T>(string key, out T value)
        {
            object result = provider.Get(Prefix(key));
            
            if (result == null)
            {
                value = default(T);
                return false;
            }

            value = (T)result;
            
            return true;
        }
    }
}
