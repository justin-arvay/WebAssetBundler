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

    public class BundleMetadataCachePrimer : IBundleMetadataCachePrimer
    {
        private IConfigurationDriver driver;
        private IBundleMetadataCache cache;
        private static bool isPrimed = false;
        static readonly object locker = new object();

        public BundleMetadataCachePrimer(IConfigurationDriver driver, IBundleMetadataCache cache)
        {
            this.driver = driver;
            this.cache = cache;
        }

        public bool IsPrimed
        {
            get
            {
                return isPrimed;
            }
        }


        public void Prime()
        {
            //lock to ensure we dont prime the cache twice. would be possible if two requests happen at the same time            
            lock (locker)
            {
                foreach (var metadata in driver.LoadMetadata())
                {
                    cache.Add(metadata);
                }

                isPrimed = true;
            }
        }
    }
}
