namespace ResourceCompiler.Files
{
    public interface IFileReaderFactory
    {
        IFileReader GetFileReader(string file);
        bool FileExists(string file);
    }
}