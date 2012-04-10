using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ResourceCompiler.Web.Mvc
{
    public class JavaScriptWebAssetMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;

        public JavaScriptWebAssetMerger(IWebAssetReader reader)
        {
            this.reader = reader;            
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
         
            return new WebAssetMergerResult(resolverResult.Path, content);
        }
    }
}
