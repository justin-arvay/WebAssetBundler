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
using System.Web;
    using System.IO;

    public class StyleSheetBundleProvider : IBundleProvider<StyleSheetBundle>
    {
        private IConfigProvider<StyleSheetBundleConfiguration> configProvider;
        private IBundlesCache<StyleSheetBundle> cache;
        private IAssetProvider assetProvider;
        private IBundlePipeline<StyleSheetBundle> pipeline;
        private BundleContext context;
        private IBundleCachePrimer<StyleSheetBundle, StyleSheetBundleConfiguration> primer;

        public StyleSheetBundleProvider(IConfigProvider<StyleSheetBundleConfiguration> configProvider, IBundlesCache<StyleSheetBundle> cache, 
            IBundlePipeline<StyleSheetBundle> pipeline, IAssetProvider assetProvider, BundleContext context,
            IBundleCachePrimer<StyleSheetBundle, StyleSheetBundleConfiguration> primer)
        {
            this.configProvider = configProvider;
            this.cache = cache;
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.context = context;
            this.primer = primer;
        }       

        public StyleSheetBundle GetNamedBundle(string name)
        {
            if (primer.IsPrimed == false || context.DebugMode)
            {
                primer.Prime(configProvider.GetConfigs());
            }

            return cache.Get(name);
        }

        public StyleSheetBundle GetSourceBundle(string source)
        {
            var name = source.ToHash() + "-" + Path.GetFileNameWithoutExtension(source).Replace(".", "-");
            var bundle = cache.Get(name);

            if (bundle == null || context.DebugMode)
            {
                var asset = assetProvider.GetAsset(source);
                bundle = new StyleSheetBundle();
                bundle.Assets.Add(asset);
                bundle.Name = name;

                pipeline.Process(bundle);
                cache.Add(bundle);
            }

            return bundle;
        }
    }
}
