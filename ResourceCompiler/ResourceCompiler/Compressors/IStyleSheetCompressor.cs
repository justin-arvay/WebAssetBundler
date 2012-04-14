namespace ResourceCompiler.Web.Mvc
{
    public interface IStyleSheetCompressor
    {        
        string CompressContent(string content);
    }
}