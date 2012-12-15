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

    public class ScriptBundleProvider : IBundleProvider<ScriptBundle>
    {
        private IScriptConfigProvider configProvider;
        private IBundlesCache<ScriptBundle> cache;
        private IAssetProvider assetLocator;
        IBundlePipeline<ScriptBundle> pipeline;

        public ScriptBundleProvider(IScriptConfigProvider configProvider, IBundlesCache<ScriptBundle> cache, 
            IAssetProvider assetLocator, IBundlePipeline<ScriptBundle> pipeline)
        {
            this.configProvider = configProvider;
            this.cache = cache;
            this.assetLocator = assetLocator;
            this.pipeline = pipeline;
        }

        public ScriptBundle GetBundle(string name)
        {
            var bundles = cache.Get();

            if (bundles == null)
            {
                bundles = new BundleCollection<ScriptBundle>();
                foreach (ScriptBundleConfiguration item in configProvider.GetConfigs())
                {
                    item.AssetProvider = assetLocator;
                    item.Configure();
                    bundles.Add(item.GetBundle());
                }

                cache.Set(bundles);
            }

            return bundles.FindBundleByName(name);
        }
    }
}
