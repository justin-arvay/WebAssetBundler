// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Collections.Generic;
    using WebAssetBundler.Web.Mvc;
    using System.Linq;

    public class WebAssetBundleResolver : IWebAssetResolver
    {
        private Bundle bundle;        

        public WebAssetBundleResolver(Bundle bundle)
        {
            this.bundle = bundle;            
        }

        public IList<ResolvedBundle> Resolve()
        {
            var results = new List<ResolvedBundle>();

            foreach (var asset in bundle.Assets)
            {
                results.Add(ResolveWebAsset(asset.Name, bundle.Compress, bundle.Host, asset));
            }

            return results;
        }

        private ResolvedBundle ResolveWebAsset(string name, bool compress, string host, AssetBase webAsset)
        {
            var assets = new List<AssetBase>();
            assets.Add(webAsset);

            return new ResolvedBundle(assets, name)
                {
                    Compress = compress,
                    Host = host
                };
        }
    }
}
