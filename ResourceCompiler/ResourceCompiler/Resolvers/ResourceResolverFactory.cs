namespace ResourceCompiler.Resolvers
{
    using System.Linq;
    using ResourceCompiler.Resource;

    public class ResourceResolverFactory : IResourceResolverFactory
    {

        public IResourceResolver Create(ResourceGroup resourceGroup)
        {
            if (resourceGroup.Combined)
            {
                return new CombinedResourceGroupResolver(resourceGroup);
            }

            return new ResourceGroupResolver(resourceGroup);
        }
    }
}
