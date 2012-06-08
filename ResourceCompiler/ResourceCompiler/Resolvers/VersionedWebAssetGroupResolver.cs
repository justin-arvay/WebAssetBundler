// ResourceCompiler - Compiles web assets so you dont have to.
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VersionedWebAssetGroupResolver : IWebAssetResolver
    {
        private IPathResolver pathResolver;
        private WebAssetGroup webAssetGroup;

        public VersionedWebAssetGroupResolver(WebAssetGroup webAssetGroup, IPathResolver pathResolver)
        {
            this.webAssetGroup = webAssetGroup;
            this.pathResolver = pathResolver;
        }

        public IList<ResolverResult> Resolve()
        {
            var results = new List<ResolverResult>();

            foreach (var webAsset in webAssetGroup.Assets)
            {
                results.Add(ResolveWebAsset(webAssetGroup.Version, webAssetGroup.Compress, webAsset));
            }

            return results;
        }

        private ResolverResult ResolveWebAsset(string version, bool compress, IWebAsset webAsset)
        {
            var path = pathResolver.Resolve(webAssetGroup.GeneratedPath, version, webAsset.Name);
            var assets = new List<IWebAsset>();
            assets.Add(webAsset);

            return new ResolverResult(path, compress, assets);
        }
    }
}
