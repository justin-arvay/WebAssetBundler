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
        private IBundlePipeline<ImageBundle> pipeline;
        private SettingsContext settings;
        private IBundleFactory<ImageBundle> bundleFactory;

        public ImageBundleProvider(IBundlesCache<ImageBundle> cache, IBundlePipeline<ImageBundle> pipeline, 
            IBundleFactory<ImageBundle> bundleFactory, SettingsContext settings)
        {
            this.cache = cache;
            this.pipeline = pipeline;
            this.settings = settings;
            this.bundleFactory = bundleFactory;
        }

        public ImageBundle GetNamedBundle(string name)
        {
            throw new NotSupportedException();
        }

        public ImageBundle GetSourceBundle(string source)
        {
            string name = ImageHelper.CreateBundleName(source);
            string contentType = ImageHelper.GetContentType(source);
            ImageBundle bundle = cache.Get(name);
            
            if (bundle == null || settings.DebugMode)
            {
                bundle = bundleFactory.CreateFromSource(source);

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
