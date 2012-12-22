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

    public class StyleSheetBundleProvider : IBundleProvider<StyleSheetBundle>
    {
        private IStyleSheetConfigProvider configProvider;
        private IBundlesCache<StyleSheetBundle> cache;
        private IAssetProvider assetLocator;
        private IBundlePipeline<StyleSheetBundle> pipeline;
        private HttpServerUtilityBase server;

        public StyleSheetBundleProvider(IStyleSheetConfigProvider configProvider, IBundlesCache<StyleSheetBundle> cache, 
            IBundlePipeline<StyleSheetBundle> pipeline, IAssetProvider assetLocator, HttpServerUtilityBase server)
        {
            this.configProvider = configProvider;
            this.cache = cache;
            this.assetLocator = assetLocator;
            this.pipeline = pipeline;
            this.server = server;
        }       

        public StyleSheetBundle GetBundle(string name)
        {
            //TODO:: resolve bundles

            var bundles = cache.Get();

            if (bundles == null)
            {
                bundles = new BundleCollection<StyleSheetBundle>();
                foreach (StyleSheetBundleConfiguration item in configProvider.GetConfigs())
                {
                    item.AssetProvider = assetLocator;
                    item.Configure();

                    var bundle = item.GetBundle();
                    pipeline.Process(bundle);

                    bundles.Add(bundle);
                }

                cache.Set(bundles);
            }

            return bundles.FindBundleByName(name);
        }

        public StyleSheetBundle GetBundle(string source)
        {
            var asset = new FileAsset(new AssetFile(source, server));
            var bundle = new StyleSheetBundle();
            bundle.Assets.Add(asset);

            pipeline.Process(bundle);

            return bundle;
        }
    }
}
