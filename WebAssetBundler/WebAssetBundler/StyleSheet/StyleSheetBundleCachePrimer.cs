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

    public class StyleSheetBundleCachePrimer : IBundleCachePrimer<StyleSheetBundle>
    {
        private IBundlesCache<StyleSheetBundle> cache;
        private IAssetProvider<StyleSheetBundle> assetProvider;
        private IBundlePipeline<StyleSheetBundle> pipeline;

        private static bool isPrimed = false;

        public StyleSheetBundleCachePrimer(IAssetProvider<StyleSheetBundle> assetProvider, IBundlePipeline<StyleSheetBundle> pipeline,
            IBundlesCache<StyleSheetBundle> cache)
        {
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.cache = cache;
        }

        public bool IsPrimed
        {
            get
            {
                return isPrimed;
            }
        }


        public void Prime(IList<IBundleConfiguration<StyleSheetBundle>> configs)
        {
            foreach (IBundleConfiguration<StyleSheetBundle> item in configs)
            {
                item.Bundle = new StyleSheetBundle();
                item.Bundle.Name = item.GetType().Name;
                item.AssetProvider = assetProvider;
                item.Configure();

                pipeline.Process(item.Bundle);
                cache.Add(item.Bundle);
            }

            isPrimed = true;
        }
    }
}
