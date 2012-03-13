
namespace ResourceCompiler.Resolvers
{
    using System.Collections.Generic;
    using ResourceCompiler.WebAsset;
    using System.Linq;

    public class WebAssetGroupCollectionResolver : IWebAssetGroupCollectionResolver
    {
        private IWebAssetResolverFactory resolverFactory;
        private IUrlResolver urlResolver;

        public WebAssetGroupCollectionResolver(IUrlResolver urlResolver, IWebAssetResolverFactory resolverFactory)
        {
            this.resolverFactory = resolverFactory;
            this.urlResolver = urlResolver;
        }

        /// <summary>
        /// Resolves all the resource groups into a collection of urls.
        /// </summary>
        /// <param name="resourceGroups"></param>
        /// <returns></returns>
        public IEnumerable<string> Resolve(WebAssetGroupCollection resourceGroups)
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
