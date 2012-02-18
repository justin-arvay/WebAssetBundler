namespace ResourceCompiler.Compressors.JavaScript
{
    public interface IJavaScriptCompressor
    {
        string Identifier { get; }        
        string CompressContent(string content);
    }
}