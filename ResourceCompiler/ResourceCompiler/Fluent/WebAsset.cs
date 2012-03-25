
namespace ResourceCompiler.Web.Mvc
{
    using System.IO;
    using System;

    public class WebAsset : IWebAsset
    {
        public WebAsset(string source)
        {
            Source = source;
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(Source);
            }
        }

        public string Version
        {
            get;
            set;
        }

        public string Source
        {
            get;
            private set;
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(Source);
            }
        }
    }
}