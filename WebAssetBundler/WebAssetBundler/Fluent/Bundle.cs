namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Bundle
    {
        public string Name { get; set; }
        public bool Combine { get; set; }
        public bool Compress { get; set; }
        public WebAssetType Type { get; set; }

        public IList<WebAsset> Assets { get; set; }
    }
}
