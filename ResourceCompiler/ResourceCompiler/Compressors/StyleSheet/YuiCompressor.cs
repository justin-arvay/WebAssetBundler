
using Yahoo.Yui.Compressor;

namespace ResourceCompiler.Compressors.StyleSheet
{
    public class YuiCompressor: IStyleSheetCompressor
    {
        public static string Identifier
        {
            get { return "YuiCompressor"; }
        }

        public string CompressContent(string content)
        {
            return CssCompressor.Compress(content, 0, CssCompressionType.StockYuiCompressor);
        }

        string IStyleSheetCompressor.Identifier
        {
            get { return Identifier; }
        }
    }
}
