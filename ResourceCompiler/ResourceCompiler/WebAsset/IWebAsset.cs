using System;
namespace ResourceCompiler.WebAsset
{
    public interface IWebAsset
    {
        string Source { get; }
        bool Exists();
        DateTime GetLastWrite();
    }
}
