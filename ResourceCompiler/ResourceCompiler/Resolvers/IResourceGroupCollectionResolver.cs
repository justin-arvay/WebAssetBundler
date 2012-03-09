
namespace ResourceCompiler.Resolvers
{
    using ResourceCompiler.Resource;
    using System.Collections.Generic;

    public interface IResourceGroupCollectionResolver
    {
        IEnumerable<string> Resolve(ResourceGroupCollection resources);
    }
}
