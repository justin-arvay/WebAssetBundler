
namespace ResourceCompiler.Web.Mvc
{
    using System;

    public interface IWebAsset
    {
        string Source { get; }
        string FileName { get; }
        string Extension { get; }
    }
}
