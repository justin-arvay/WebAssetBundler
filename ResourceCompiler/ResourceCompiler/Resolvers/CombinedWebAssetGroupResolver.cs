
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

        public IList<WebAssetResolverResult> Resolve()
        {
            var path = pathResolver.Resolve(DefaultSettings.GeneratedFilesPath, webAssetGroup.Version, webAssetGroup.Name);
            var results = new List<WebAssetResolverResult>();

            results.Add(new WebAssetResolverResult(path, webAssetGroup.Assets));

            return results;
        }

    }
}
