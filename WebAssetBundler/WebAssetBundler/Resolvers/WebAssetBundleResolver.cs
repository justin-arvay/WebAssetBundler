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

        public IList<ResolverResult> Resolve()
        {
            var results = new List<ResolverResult>();

            foreach (var asset in bundle.Assets)
            {
                results.Add(ResolveWebAsset(asset.Name, bundle.Compress, bundle.Host, asset));
            }

            return results;
        }

        private ResolverResult ResolveWebAsset(string name, bool compress, string host, WebAsset webAsset)
        {
            var assets = new List<WebAsset>();
            assets.Add(webAsset);

            return new ResolverResult(assets, name)
                {
                    Compress = compress,
                    Host = host
                };
        }
    }
}
