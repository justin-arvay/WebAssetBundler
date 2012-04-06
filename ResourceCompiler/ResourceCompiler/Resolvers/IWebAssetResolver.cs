
namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;

    public interface IWebAssetResolver
    {
        ICollection<WebAssetResolverResult> Resolve();


    }
}
