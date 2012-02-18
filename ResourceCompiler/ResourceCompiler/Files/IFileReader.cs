using System;

namespace ResourceCompiler.Files
{
    public interface IFileReader: IDisposable
    {
        string ReadLine();
        string ReadToEnd();
    }
}