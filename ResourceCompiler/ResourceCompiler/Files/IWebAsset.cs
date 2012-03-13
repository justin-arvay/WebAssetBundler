using System;
namespace ResourceCompiler.Files
{
    public interface IWebAsset
    {
        string Source { get; }
        bool Exists();
        DateTime GetLastWrite();
    }
}
