
namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;

    public interface IWebAssetResolver
    {
        IEnumerable<string> Resolve();
    }
}
