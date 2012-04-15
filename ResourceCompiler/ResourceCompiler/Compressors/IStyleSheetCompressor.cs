namespace ResourceCompiler.Web.Mvc
{
    public interface IStyleSheetCompressor
    {        
        string Compress(string content);
    }
}