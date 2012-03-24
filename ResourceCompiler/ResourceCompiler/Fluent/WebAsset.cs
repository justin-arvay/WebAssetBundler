
namespace ResourceCompiler.Web.Mvc
{
    using System.IO;
    using System;

    public class WebAsset : IWebAsset
    {
        public string Source { get; private set; }

        public WebAsset(string source)
        {
            Source = source;
        }
    }
}