
namespace ResourceCompiler.Resolvers
{
    using ResourceCompiler.Resource;

    public interface IResourceResolverFactory
    {
        IResourceResolver Create(ResourceGroup resourceGroup);
    }
}
