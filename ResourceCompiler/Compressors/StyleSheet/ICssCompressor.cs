namespace ResourceCompiler.Compressors.StyleSheet
{
    public interface IStyleSheetCompressor
    {
        string Identifier { get; }
        string CompressContent(string content);
    }
}