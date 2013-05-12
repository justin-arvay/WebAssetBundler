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
        private IBundlesCache<ImageBundle> cache;
        private IAssetProvider assetProvider;
        private IBundlePipeline<ImageBundle> pipeline;
        private SettingsContext settings;

        public ImageBundleProvider(IBundlesCache<ImageBundle> cache, IAssetProvider assetProvider,
            IBundlePipeline<ImageBundle> pipeline, SettingsContext settings)
        {
            this.cache = cache;
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.settings = settings;
        }

        public ImageBundle GetNamedBundle(string name)
        {
            throw new NotSupportedException();
        }

        public ImageBundle GetSourceBundle(string source)
        {
            var bundle = cache.Get("");
            var contentType = ImageHelper.GetContentType(source);

            if (bundle == null || settings.DebugMode)
            {
                var asset = assetProvider.GetAsset(source);
                bundle = new ImageBundle(contentType, );
                bundle.Assets.Add(asset);
                bundle.Name = name;

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
