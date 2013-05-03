namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Linq;
    using System.IO;

    public abstract class Bundle
    {
        public Bundle()
        {
            Minify = true;
            Assets = new AssetCollection();
            BrowserTtl = 525949; //1 year default
            Host = "";
        }

        public string Name { get; set; }
        public bool Minify { get; set; }
        public WebAssetType Type { get; protected set; }
        public string Host { get; set; }
        public string Extension { get; set; }
        public abstract string ContentType { get; }
        public int BrowserTtl { get; set; }
        public string Url { get; set; }

        public AssetCollection Assets { get; set; }

        public Stream Content
        {
            get
            {
                if (Assets.Count == 0)
                {
                    return Stream.Null;
                }

                return Assets[0].Content;
            }
        }

        public bool IsExternal
        {
            get
            {
                return Assets.Count == 1 && Assets[0] is ExternalAsset;
            }
        }

        public byte[] Hash
        {
            get;
            set;
        }
    }
}
