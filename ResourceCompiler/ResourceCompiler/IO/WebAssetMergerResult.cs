using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class WebAssetMergerResult
    {
        public WebAssetMergerResult(string path, string content)
        {
            Path = path;
            Content = content;
        }

        public string Path
        {
            get;
            private set;
        }

        public string Content
        {
            get;
            private set;
        }
    }
}
