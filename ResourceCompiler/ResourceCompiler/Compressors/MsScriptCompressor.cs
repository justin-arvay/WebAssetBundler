
namespace ResourceCompiler.Web.Mvc
{
    using Microsoft.Ajax.Utilities;

    public class MsScriptCompressor: IScriptCompressor
    {
        public string Compress(string content)
        {
            var minifer = new Minifier();
            return minifer.MinifyJavaScript(content);
        }
    }
}