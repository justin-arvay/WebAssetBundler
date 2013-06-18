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

    public class StyleSheetBundleProvider : BundleProviderBase<StyleSheetBundle>
    {
        private IBundleCache<StyleSheetBundle> cache;
        private IAssetProvider assetProvider;
        private IBundlePipeline<StyleSheetBundle> pipeline;
        private IBundleMetadataProvider metadataProvider;
        private IBundleFactory<StyleSheetBundle> factory;

        public StyleSheetBundleProvider(IBundleCache<StyleSheetBundle> cache, IBundlePipeline<StyleSheetBundle> pipeline, 
            IBundleMetadataProvider metadataProvider, IBundleFactory<StyleSheetBundle> factory,
            IAssetProvider assetProvider, SettingsContext settings)
            : base(settings)
        {
            this.cache = cache;
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.metadataProvider = metadataProvider;
            this.factory = factory;
        }       

        public override StyleSheetBundle GetNamedBundle(string name)
        {
            StyleSheetBundle bundle = cache.Get(name);
            BundleMetadata metadata = null;

            if (bundle == null || Settings.DebugMode)
            {
                metadata = metadataProvider.GetMetadata<StyleSheetBundle>(name);
                bundle = factory.Create(metadata);
                pipeline.Process(bundle);
                cache.Add(bundle);
            }

            return bundle;
        }

        public override StyleSheetBundle GetSourceBundle(string source)
        {
            var name = source.ToHash() + "-" + Path.GetFileNameWithoutExtension(source).Replace(".", "-");
            var bundle = cache.Get(name);

            if (bundle == null || Settings.DebugMode)
            {
                var asset = assetProvider.GetAsset(source);
                bundle = factory.Create(asset);

                pipeline.Process(bundle);
                cache.Add(bundle);
            }

            return bundle;
        }
    }
}
