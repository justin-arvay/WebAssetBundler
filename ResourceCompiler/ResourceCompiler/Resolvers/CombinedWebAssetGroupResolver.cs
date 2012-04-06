
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
    using System.Linq;

    public class CombinedWebAssetGroupResolver : IWebAssetResolver
    {
        private WebAssetGroup webAssetGroup;
        private IPathResolver pathResolver;

        public CombinedWebAssetGroupResolver(WebAssetGroup webAssetGroup, IPathResolver pathResolver)
        {
            this.webAssetGroup = webAssetGroup;
            this.pathResolver = pathResolver;
        }

        public ICollection<WebAssetResolverResult> Resolve()
        {
            var path = pathResolver.Resolve("", webAssetGroup.Version, webAssetGroup.Name, "css");
            var results = new List<WebAssetResolverResult>();

            results.Add(new WebAssetResolverResult(path, webAssetGroup.Assets));

            return results;
        }

    }
}
