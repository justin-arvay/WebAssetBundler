
namespace ResourceCompiler.WebAsset
{
    using System.IO;
    using System;

    public class SourceWebAsset : IWebAsset
    {
        public string Source { get; private set; }

        public SourceWebAsset(string path)
        {
            Source = path;
        }
    }
}