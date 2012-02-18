using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Compressors.StyleSheet;
using ResourceCompiler.Files;

namespace ResourceCompiler.Assets
{
    public interface IStyleSheetAssets
    {
        IStyleSheetAssets Combine(bool value);
        IStyleSheetAssets Compress(bool value);
        IStyleSheetAssets Version(bool value);
        IStyleSheetAssets Add(string path);
        IStyleSheetAssets AddDynamic(string path);
        IStyleSheetAssets Media(string value);
        IStyleSheetAssets SetCompressor(IStyleSheetCompressor compressor);
        IStyleSheetAssets RendererUrl(string url);
        IList<IResource> GetFiles();

        bool Versioned { get; set; }
        bool Combined { get; set; }
        bool Compressed { get; set; }
        string MediaType { get; set; }
        IStyleSheetCompressor Compressor { get; set; }
        string GetLastWriteTimestamp();
    }
}
