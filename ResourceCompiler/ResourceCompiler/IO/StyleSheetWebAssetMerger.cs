using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class StyleSheetWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;
        private IWebAssetContentFilter filter;

        public StyleSheetWebAssetMerger(IWebAssetReader reader, IWebAssetContentFilter filter)
        {
            this.reader = reader;
            this.filter = filter;
        }

        public WebAssetMergerResult Merge(WebAssetResolverResult resolverResult)
        {
            string content = "";

            foreach (var webAsset in resolverResult.WebAssets)
            {
                content += reader.Read(webAsset);
            }

            return new WebAssetMergerResult(resolverResult.Path, content);
        }
    }
}
