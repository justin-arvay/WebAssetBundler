
namespace ResourceCompiler.Resolvers
{
    using ResourceCompiler.WebAsset;
    using System.Collections.Generic;

    public interface IWebAssetGroupCollectionResolver
    {
        IEnumerable<string> Resolve(WebAssetGroupCollection resources);
    }
}
