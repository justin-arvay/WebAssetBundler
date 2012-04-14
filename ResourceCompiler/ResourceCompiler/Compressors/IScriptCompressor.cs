namespace ResourceCompiler.Web.Mvc
{
    public interface IScriptCompressor
    {  
        string Compress(string content);
    }
}