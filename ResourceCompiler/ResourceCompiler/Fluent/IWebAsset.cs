
namespace ResourceCompiler.Web.Mvc
{
    using System;

    public interface IWebAsset
    {
        string Source { get; }
        string Name { get; }
        string Extension { get; }
    }
}
