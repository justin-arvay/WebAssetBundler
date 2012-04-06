

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;


    public class WebAssetResolverResult
    {

        public WebAssetResolverResult(string path, ICollection<IWebAsset> webAssets)
        {
            Path = path;
            WebAssets = webAssets;
        }

        public string Path
        {
            get;
            private set;
        }

        public ICollection<IWebAsset> WebAssets
        {
            get;
            private set;
        }
    }
}