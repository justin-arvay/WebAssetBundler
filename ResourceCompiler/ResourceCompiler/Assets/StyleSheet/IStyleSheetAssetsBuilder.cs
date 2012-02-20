using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Compressors.StyleSheet;
using ResourceCompiler.Files;

namespace ResourceCompiler.Assets
{
    public interface IStyleSheetAssetsBuilder
    {
        IStyleSheetAssetsBuilder Combine(bool value);
        IStyleSheetAssetsBuilder Compress(bool value);
        IStyleSheetAssetsBuilder Version(bool value);
        IStyleSheetAssetsBuilder Add(string path);
        IStyleSheetAssetsBuilder AddDynamic(string path);
        IStyleSheetAssetsBuilder Media(string value);
        IStyleSheetAssetsBuilder SetCompressor(IStyleSheetCompressor compressor);
        IStyleSheetAssetsBuilder RendererUrl(string url);
        IList<IResource> GetFiles();

        bool Versioned { get; set; }
        bool Combined { get; set; }
        bool Compressed { get; set; }
        string MediaType { get; set; }
        IStyleSheetCompressor Compressor { get; set; }
        string GetLastWriteTimestamp();
    }
}
