
namespace ResourceCompiler.Web.Mvc
{
    using ResourceCompiler.Web.Mvc;
    using System.Collections.Generic;

    public interface IWebAssetGroupCollectionResolver
    {
        ICollection<WebAssetResolverResult> Resolve(WebAssetGroupCollection resources);
    }
}
