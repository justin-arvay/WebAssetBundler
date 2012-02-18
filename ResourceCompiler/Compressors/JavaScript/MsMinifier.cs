
using Microsoft.Ajax.Utilities;

namespace ResourceCompiler.Compressors.JavaScript
{
    public class MsMinifier: IJavaScriptCompressor
    {
        string IJavaScriptCompressor.Identifier
        {
            get { return Identifier; }
        }
        
        public static string Identifier
        {
            get { return "MsJavaScriptMinifier"; }
        }

        public string CompressContent(string content)
        {
            var minifer = new Minifier();
            return minifer.MinifyJavaScript(content);
        }
    }
}