
namespace ResourceCompiler.Web.Mvc
{
    using Microsoft.Ajax.Utilities;

    public class MsStyleSheetCompressor: IStyleSheetCompressor
    {

        public string Compress(string content)
        {
            var minifier = new Minifier();
            return minifier.MinifyStyleSheet(content);
        }
    }
}
