

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;


    public class WebAssetResolverResult
    {

        public WebAssetResolverResult(string path, bool compress, ICollection<IWebAsset> webAssets)
        {
            Path = path;
            WebAssets = webAssets;
            Compress = compress;
        }

        public string Path
        {
            get;
            private set;
        }

        public bool Compress
        {
            get;
            set;
        }

        public ICollection<IWebAsset> WebAssets
        {
            get;
            private set;
        }
    }
}