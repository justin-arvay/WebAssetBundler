namespace ResourceCompiler.Compressors.StyleSheet
{
    public class NullCompressor: IStyleSheetCompressor
    {
        public static string Identifier
        {
            get { return "NullCompressor"; }
        }

        public string CompressContent(string content)
        {
            return content;
        }

        string IStyleSheetCompressor.Identifier
        {
            get { return Identifier; }
        }        
    }
}