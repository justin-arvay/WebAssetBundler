using System;
namespace ResourceCompiler.Files
{
    public interface IResource
    {
        string Source { get; }
        bool Exists();
        DateTime GetLastWrite();
    }
}
