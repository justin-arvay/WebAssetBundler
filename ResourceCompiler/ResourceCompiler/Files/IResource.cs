using System;
namespace ResourceCompiler.Files
{
    public interface IResource
    {
        string Path { get; }
        bool Exists();
        DateTime GetLastWrite();
    }
}
