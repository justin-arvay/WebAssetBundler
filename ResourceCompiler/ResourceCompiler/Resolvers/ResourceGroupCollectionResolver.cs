
namespace ResourceCompiler.Resolvers
{
    using System.Collections.Generic;
    using ResourceCompiler.Resource;
    using System.Linq;

    public class ResourceGroupCollectionResolver : IResourceGroupCollectionResolver
    {
        private IResourceResolverFactory resolverFactory;
        private IUrlResolver urlResolver;

        public ResourceGroupCollectionResolver(IUrlResolver urlResolver, IResourceResolverFactory resolverFactory)
        {
            this.resolverFactory = resolverFactory;
            this.urlResolver = urlResolver;
        }

        /// <summary>
        /// Resolves all the resource groups into a collection of urls.
        /// </summary>
        /// <param name="resourceGroups"></param>
        /// <returns></returns>
        public IEnumerable<string> Resolve(ResourceGroupCollection resourceGroups)
        {
            var urls = new List<string>();

            foreach (var resourceGroup in resourceGroups)
            {
                var resolver = resolverFactory.Create(resourceGroup);
                var groupUrls = resolver.Resolve();
               
                //
                groupUrls = groupUrls.Select((url) => urlResolver.Resolve(url));
                urls.AddRange(groupUrls);
            }


            return urls;
        }
    }
}
