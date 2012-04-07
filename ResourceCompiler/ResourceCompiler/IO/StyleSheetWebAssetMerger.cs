using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class StyleSheetWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;

        public StyleSheetWebAssetMerger(IWebAssetReader reader)
        {
            this.reader = reader;
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
