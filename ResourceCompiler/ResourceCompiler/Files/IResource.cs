using System;
namespace ResourceCompiler.Files
{
    public interface IResource
    {
        string Path { get; }
        string Type { get; }
        bool Exists();
        DateTime GetLastWrite();
    }
}
