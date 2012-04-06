﻿
namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
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
        public IEnumerable<WebAssetResolverResult> Resolve(WebAssetGroupCollection resourceGroups)
        {
            var results = new List<WebAssetResolverResult>();

            foreach (var resourceGroup in resourceGroups)
            {
                var resolver = resolverFactory.Create(resourceGroup);
                results.AddRange(resolver.Resolve());
            }


            return results;
        }
    }
}
