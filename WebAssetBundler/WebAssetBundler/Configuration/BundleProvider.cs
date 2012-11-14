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
    using System.Collections.Generic;

    public class BundleProvider<TBundle> : IBundleProvider<TBundle> where TBundle : Bundle
    {
        private BuilderContext context;
        private IConfigurationFactory factory;
        private IBundlesCache<TBundle> bundlesCache;

        public BundleProvider(BuilderContext context, IConfigurationFactory factory, IBundlesCache<TBundle> bundlesCache)
        {
            this.context = context;
            this.bundlesCache = bundlesCache;
            this.factory = factory;
        }

        public BundleCollection GetBundles() 
        {
            var bundles = new List<TBundle>();

            IList<StyleSheetBundleConfiguration> configs = factory.Create<StyleSheetBundleConfiguration, TBundle>(context);                
            
            foreach(var config in configs)
            {
                config.Configure();
                bundles.Add(config.GetBundle());
            }

            return new BundleCollection(bundles);
        }
    }
}
