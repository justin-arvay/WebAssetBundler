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
    using System.Web;

    public class StyleSheetWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;
        private IContentFilter filter;
        private IStyleSheetCompressor compressor;
        private HttpServerUtilityBase server;

        public StyleSheetWebAssetMerger(IWebAssetReader reader, IContentFilter filter, IStyleSheetCompressor compressor, HttpServerUtilityBase server)
        {
            this.reader = reader;
            this.filter = filter;
            this.server = server;
            this.compressor = compressor;
        }

        public WebAssetMergerResult Merge(ResolverResult resolverResult)
        {
            string content = "";

            foreach (var webAsset in resolverResult.WebAssets)
            {
                content += filter.Filter(server.MapPath(resolverResult.Path), server.MapPath(webAsset.Source), reader.Read(webAsset));
            }

            if (resolverResult.Compress)
            {
                content = compressor.Compress(content);
            }

            return new WebAssetMergerResult(resolverResult.Path, content);
        }
    }
}
