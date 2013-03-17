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
        private IBundleConfigurationProvider<ScriptBundle> configProvider;
        private IBundlesCache<ScriptBundle> cache;
        private IAssetProvider<ScriptBundle> assetProvider;
        private IBundlePipeline<ScriptBundle> pipeline;
        private IBundleCachePrimer<ScriptBundle> primer;

        public ScriptBundleProvider(IBundleConfigurationProvider<ScriptBundle> configProvider, IBundlesCache<ScriptBundle> cache,
            IAssetProvider<ScriptBundle> assetProvider, IBundlePipeline<ScriptBundle> pipeline, IBundleCachePrimer<ScriptBundle> primer,
            WabSettings<ScriptBundle> settings)
            : base(settings)
        {
            this.configProvider = configProvider;
            this.cache = cache;
            this.assetProvider = assetProvider;
            this.pipeline = pipeline;
            this.primer = primer;
        }

        public override ScriptBundle GetNamedBundle(string name)
        {
            if (primer.IsPrimed == false || Settings.DebugMode)
            {
                primer.Prime(configProvider.GetConfigs());
            }
            
            return cache.Get(name);
        }

        public override ScriptBundle GetSourceBundle(string source)
        {
            var name = source.ToHash() + "-" + Path.GetFileNameWithoutExtension(source).Replace(".", "-");
            var bundle = cache.Get(name);

            if (bundle == null || Settings.DebugMode)
            {
                var asset = assetProvider.GetAsset(source);
                bundle = new ScriptBundle();
                bundle.Assets.Add(asset);
                bundle.Name = name;

                pipeline.Process(bundle);
                cache.Add(bundle);
            }

            return bundle;
        }
    }
}
