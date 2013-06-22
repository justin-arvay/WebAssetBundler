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

    public class BundleProvider<TBundle> : IBundleProvider<TBundle> 
        where TBundle : Bundle, new()
    {
        protected IBundleCache<TBundle> cache;
        protected IAssetProvider assetProvider;
        protected IBundlePipeline<TBundle> pipeline;
        protected IBundleFactory<TBundle> factory;
        protected IConfigurationDriver driver;
        protected SettingsContext settings;

        public BundleProvider(IBundleCache<TBundle> cache, IBundleFactory<TBundle> factory,
            IConfigurationDriver driver, IAssetProvider assetProvider, IBundlePipeline<TBundle> pipeline,
            SettingsContext settings)
        {
            this.settings = settings;
            this.cache = cache;
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.driver = driver;
            this.factory = factory;
        }

        public virtual TBundle GetExternalBundle(string source) 
        {
            var bundle = new TBundle();
            bundle.Name = source.ToHash();
            bundle.Assets.Add(new ExternalAsset()
            {
                Source = source,
            });

            return bundle;
        }

        public virtual TBundle GetNamedBundle(string name)
        {
            TBundle bundle = cache.Get(name);

            if (bundle == null || settings.DebugMode)
            {
                bundle = driver.LoadBundle<TBundle>(name);
                pipeline.Process(bundle);
                cache.Add(bundle);
            }

            return bundle;
        }

        public virtual TBundle GetSourceBundle(string source)
        {
            var bundle = cache.Get(AssetHelper.GetBundleName(source));

            if (bundle == null || settings.DebugMode)
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
