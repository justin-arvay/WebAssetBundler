

namespace ResourceCompiler.Web.Mvc
{
    using Yahoo.Yui.Compressor;

    public class YuiStyleSheetCompressor: IStyleSheetCompressor
    {
        public string CompressContent(string content)
        {
            return CssCompressor.Compress(content, 0, CssCompressionType.StockYuiCompressor);
        }
    }
}
