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
    using System.Collections.Generic;

    public class BundlesCache<TBundle> : IBundlesCache<TBundle> where TBundle : Bundle
    {
        private ICacheProvider provider;

        public BundlesCache(ICacheProvider provider)
        {
            this.provider = provider;
        }
        
        private string GetSingleBundleKey(string bundleName)
        {
            Type typeOfBundle = typeof(TBundle);
            return bundleName + "-" + typeOfBundle.Name + "-Bundle";
        }

        public TBundle Get(string name)
        {
            var obj = provider.Get(GetSingleBundleKey(name));

            if (obj != null)
            {
                return (TBundle)obj;
            }

            return null;
        }

        public void Add(TBundle bundle)
        {
            var key = GetSingleBundleKey(bundle.Name);
            provider.Insert(key, bundle); 
        }
    }
}
