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
    using System.IO;

    public class MinifyProcessor<TBundle> : IPipelineProcessor<TBundle> 
        where TBundle : Bundle
    {
        private IMinifier minifier;
        private string minifyIdentifier;

        public MinifyProcessor(IMinifier minifier, string minifyIdentifier)
        {
            this.minifier = minifier;
            this.minifyIdentifier = minifyIdentifier;
        }
        public void Process(TBundle bundle)
        {
            if (bundle.Minify)
            {
                foreach (var asset in bundle.Assets)
                {
                    if (IsAlreadyMinified(asset) == false)
                    {
                        asset.Modify(new MinifyModifier(minifier));
                    }
                }
            }
        }

        public bool IsAlreadyMinified(AssetBase asset)
        {
            if (minifyIdentifier.Length == 0)
            {
                return false;
            }

            return Path.GetFileNameWithoutExtension(asset.Source).EndsWith(minifyIdentifier, StringComparison.OrdinalIgnoreCase);
        }
    }
}
