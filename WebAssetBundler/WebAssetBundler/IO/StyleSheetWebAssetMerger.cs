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
        private IMergedBundleCache cache;

        public StyleSheetWebAssetMerger(IWebAssetReader reader, IContentFilter filter, IStyleSheetCompressor compressor,
            HttpServerUtilityBase server, IMergedBundleCache cache)
        {
            this.reader = reader;
            this.filter = filter;
            this.server = server;
            this.compressor = compressor;
            this.cache = cache;
        }

        public IList<MergedBundle> Merge(IList<ResolvedBundle> results, BuilderContext context)
        {
            var mergedResults = new List<MergedBundle>();

            foreach (var result in results)
            {
                mergedResults.Add(MergeSingle(result, context));
            }

            return mergedResults;
        }

        private MergedBundle MergeSingle(ResolvedBundle result, BuilderContext context)
        {
            var mergedResult = cache.Get(result.Name);

            if (mergedResult == null || context.DebugMode == true)
            {
                var content = "";
                var url = (new StyleSheetUrlGenerator()).Generate("a", "a", "", context);

                foreach (var asset in result.Assets)
                {
                    content += filter.Filter(server.MapPath(url), server.MapPath(asset.Source), reader.Read(asset));
                }

                if (result.Compress)
                {
                    content = compressor.Compress(content);
                }

                mergedResult = new MergedBundle(result.Name, content, WebAssetType.StyleSheet)
                    {
                        Host = result.Host
                    };

                cache.Add(mergedResult);
            }

            return mergedResult;
        }
    }
}
