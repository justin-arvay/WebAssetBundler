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

    public class ImageBundleProvider : IBundleProvider<ImageBundle>
    {
        private IBundleCache<ImageBundle> cache;
        private IBundlePipeline<ImageBundle> pipeline;
        private SettingsContext settings;
        private IBundleFactory<ImageBundle> bundleFactory;
        private IAssetProvider assetProvider;

        public ImageBundleProvider(IBundleCache<ImageBundle> cache, IBundlePipeline<ImageBundle> pipeline, 
            IBundleFactory<ImageBundle> bundleFactory, IAssetProvider assetProvider, SettingsContext settings)
        {
            this.cache = cache;
            this.pipeline = pipeline;
            this.settings = settings;
            this.bundleFactory = bundleFactory;
            this.assetProvider = assetProvider;
        }

        public ImageBundle GetNamedBundle(string name)
        {
            throw new NotSupportedException();
        }

        public ImageBundle GetSourceBundle(string source)
        {
            AssetBase asset = assetProvider.GetAsset(source);
            string name = ImageHelper.CreateBundleName(asset);
            ImageBundle bundle = cache.Get(name);
            
            if (bundle == null || settings.DebugMode)
            {
                bundle = bundleFactory.Create(asset);

                pipeline.Process(bundle);
                cache.Add(bundle);
            }

            return bundle;
        }

        public ImageBundle GetExternalBundle(string source)
        {
            throw new NotSupportedException();
        }
    }
}
