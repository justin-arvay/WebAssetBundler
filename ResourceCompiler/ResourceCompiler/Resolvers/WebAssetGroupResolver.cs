﻿// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
    using System.Linq;

    public class WebAssetGroupResolver : IWebAssetResolver
    {
        private WebAssetGroup webAssetGroup;
        private IPathResolver pathResolver;

        public WebAssetGroupResolver(WebAssetGroup webAssetGroup)
        {
            this.webAssetGroup = webAssetGroup;            
        }

        public IList<WebAssetResolverResult> Resolve()
        {
            var results = new List<WebAssetResolverResult>();

            foreach (var webAsset in webAssetGroup.Assets)
            {
                results.Add(ResolveWebAsset(webAssetGroup.Name, webAssetGroup.Compress, webAsset));
            }

            return results;
        }

        private WebAssetResolverResult ResolveWebAsset(string name, bool compress, IWebAsset webAsset)
        {
            var assets = new List<IWebAsset>();
            assets.Add(webAsset);

            return new WebAssetResolverResult(webAsset.Source, compress, assets);
        }
    }
}
