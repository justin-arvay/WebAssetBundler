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

    public class ScriptWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;
        private IScriptCompressor compressor;
        private IPathResolver pathResolver;
        private IMergedResultCache cache;

        public ScriptWebAssetMerger(IWebAssetReader reader, IScriptCompressor compressor, IMergedResultCache cache)
        {
            this.reader = reader;
            this.compressor = compressor;
            this.cache = cache;
        }

        public IList<MergerResult> Merge(IList<ResolverResult> results)
        {
            var mergedResults = new List<MergerResult>();

            foreach (var result in results)
            {
                mergedResults.Add(MergeSingle(result));
            }

            return mergedResults;
        }

        private MergerResult MergeSingle(ResolverResult result)
        {
            var mergedResult = cache.Get(result.Name);

            if (mergedResult == null)
            {
                var content = "";

                foreach (var asset in result.Assets)
                {
                    //combined the content with a (;) 
                    //(;) ensures we end each script in case the developer forgot
                    content += reader.Read(asset) + ";";
                }

                if (result.Compress)
                {
                    //compress the merged content if we can
                    content = compressor.Compress(content);
                }

                mergedResult = new MergerResult(result.Name, result.Version, content, WebAssetType.Script)
                    {
                        Host = result.Host
                    };
                cache.Add(mergedResult);
            }

            return mergedResult;
        }        
    }
}
