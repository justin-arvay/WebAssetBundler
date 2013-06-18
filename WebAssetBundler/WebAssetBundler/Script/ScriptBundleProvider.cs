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

    public class ScriptBundleProvider : BundleProviderBase<ScriptBundle>
    {
        private BundleCache<ScriptBundle> cache;
        private IAssetProvider assetProvider;
        private IBundlePipeline<ScriptBundle> pipeline;
        private IBundleFactory<ScriptBundle> factory;
        private IBundleMetadataProvider metadataProvider;

        public ScriptBundleProvider(BundleCache<ScriptBundle> cache, IBundleFactory<ScriptBundle> factory, 
            IBundleMetadataProvider metadataProvider, IAssetProvider assetProvider, IBundlePipeline<ScriptBundle> pipeline, 
            SettingsContext settings)
            : base(settings)
        {

            this.cache = cache;
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.metadataProvider = metadataProvider;
            this.factory = factory;

        }

        public override ScriptBundle GetNamedBundle(string name)
        {
            ScriptBundle bundle = cache.Get(name);
            BundleMetadata metadata = null;

            if (bundle == null || Settings.DebugMode)
            {
                metadata = metadataProvider.GetMetadata<ScriptBundle>(name);
                bundle = factory.Create(metadata);
                cache.Add(bundle);
            }

            return bundle;
        }

        public override ScriptBundle GetSourceBundle(string source)
        {
            var bundle = cache.Get(AssetHelper.GetBundleName(source));

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
