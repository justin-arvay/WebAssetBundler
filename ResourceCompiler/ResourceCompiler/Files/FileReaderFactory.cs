using System;
using System.IO;

namespace ResourceCompiler.Files
{
    public class FileReaderFactory: IFileReaderFactory
    {
        public IFileReader GetFileReader(string file)
        {
            return new FileReader(file);
        }

        public bool FileExists(string file)
        {
            return File.Exists(file);
        }
    }
}