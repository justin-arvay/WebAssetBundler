
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using Yahoo.Yui.Compressor;
    using System.Text;
    using System.Globalization;

    public class YuiScriptCompressor: IScriptCompressor
    {      

        public string Compress(string content)
        {
            return JavaScriptCompressor.Compress(content, true, true, false, false, -1, Encoding.UTF8, CultureInfo.InvariantCulture, false);
        }
    }
}