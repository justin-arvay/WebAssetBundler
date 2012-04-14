using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ResourceCompiler.Web.Mvc
{
    public class ScriptWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;
        private IScriptCompressor compressor;

        public ScriptWebAssetMerger(IWebAssetReader reader, IScriptCompressor compressor)
        {
            this.reader = reader;
            this.compressor = compressor;
        }

        public WebAssetMergerResult Merge(WebAssetResolverResult resolverResult)
        {
            string content = "";

            foreach (var webAsset in resolverResult.WebAssets)
            {
                //combined the content with a (;) 
                //(;) ensures we end each script in case the developer forgot
                content += reader.Read(webAsset) + ";";
            }

            if (resolverResult.Compress)
            {
                //compress the merged content if we can
                content = compressor.Compress(content);
            }
         
            return new WebAssetMergerResult(resolverResult.Path, content);
        }
    }
}
