using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Files;
using ResourceCompiler.Compressors.JavaScript;

namespace ResourceCompiler.Assets
{
    public interface IJavaScriptAssetsBuilder
    {
        IJavaScriptAssetsBuilder Combine(bool value);
        IJavaScriptAssetsBuilder Compress(bool value);
        IJavaScriptAssetsBuilder Version(bool value);
        IJavaScriptAssetsBuilder Add(string path);
        IJavaScriptAssetsBuilder SetCompressor(IJavaScriptCompressor compressor);
        IJavaScriptAssetsBuilder Path(string path, Action<PathOnlyBuilder> action);
        IList<IResource> GetFiles();

        bool Versioned { get; set; }
        bool Combined { get; set; }
        bool Compressed { get; set; }
        IJavaScriptCompressor Compressor { get; set; }
        string GetLastWriteTimestamp();


    }
}
