using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ResourceCompiler.Web.Mvc
{
    public class StyleSheetWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;
        private IWebAssetContentFilter filter;
        private IStyleSheetCompressor compressor;
        private HttpServerUtilityBase server;

        public StyleSheetWebAssetMerger(IWebAssetReader reader, IWebAssetContentFilter filter, IStyleSheetCompressor compressor, HttpServerUtilityBase server)
        {
            this.reader = reader;
            this.filter = filter;
            this.server = server;
            this.compressor = compressor;
        }

        public WebAssetMergerResult Merge(WebAssetResolverResult resolverResult)
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
