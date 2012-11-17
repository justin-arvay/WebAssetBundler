﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

        public void Set(IList<TBundle> bundleCollection)
        {
            if (Get() == null)
            {
                provider.Insert(GetKey(), bundleCollection);
            }
        }

        public IList<TBundle> Get()
        {
            var dirtyBundles = provider.Get(GetKey());
            var bundles = new List<TBundle>();

            if (dirtyBundles != null)
            {
                //cast to correct type, not sure how to do this without looping
                foreach (var dirtyBundle in (IList<Bundle>)dirtyBundles)
                {
                    bundles.Add((TBundle)dirtyBundle);
                }

                return bundles;
            }

            return null;
        }

        public string GetKey()
        {
            Type typeOfBundle = typeof(TBundle);
            return typeOfBundle.Name + "-Bundles";
        }
    }
}
