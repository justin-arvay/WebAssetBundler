namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Linq;
    using System.IO;

    public abstract class Bundle
    {
        private HtmlAttributeDictionary attributes = new HtmlAttributeDictionary();

        public Bundle()
        {
            Minify = true;
            Assets = new AssetCollection();
            BrowserTtl = 525949; //1 year default
            Host = "";
        }

        /// <summary>
        /// The name and identifier of the bundle.
        /// </summary>
        public string Name { get; set; }
        public bool Minify { get; set; }
        public WebAssetType Type { get; protected set; }
        public string Host { get; set; }
        public string Extension { get; set; }     
   
        /// <summary>
        /// Time until the cache is expired in the browser in seconds.
        /// </summary>
        public int BrowserTtl { get; set; }

        /// <summary>
        /// The url to access this bundle.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The type of content being output to the browser. Used to set the Content-Type in the HTTP header.
        /// </summary>
        public abstract string ContentType { get; }

        /// <summary>
        /// The string used to separate assets when merging.
        /// </summary>
        public abstract string AssetSeparator { get; }

        public AssetCollection Assets { get; set; }

        /// <summary>
        /// A dictionary of attributes used when rending the html element. Used for additional attributes to be rendered with the html element.
        /// </summary>
        public HtmlAttributeDictionary Attributes
        {
            get
            {
                return attributes;
            }
        }

        public Stream Content
        {
            get
            {
                if (Assets.Count == 0)
                {
                    return Stream.Null;
                }

                return Assets[0].OpenStream();
            }
        }

        public void Modify(IAssetModifier modifier)
        {
            Assets.Modify(modifier);
        }

        /// <summary>
        /// Checks if the bundle is considered external.
        /// </summary>
        internal bool IsExternal
        {
            get
            {
                return Assets.Count == 1 && Assets[0] is ExternalAsset;
            }
        }

        /// <summary>
        /// A hash representing the content of the bundle. Primarily used for versioning of the bundle.
        /// </summary>
        public byte[] Hash
        {
            get;
            set;
        }
    }
}
