using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class VersionedWebAssetGroupResolver : IWebAssetResolver
    {
        private IPathResolver pathResolver;
        private WebAssetGroup webAssetGroup;

        public VersionedWebAssetGroupResolver(WebAssetGroup webAssetGroup, IPathResolver pathResolver)
        {
            this.webAssetGroup = webAssetGroup;
            this.pathResolver = pathResolver;
        }

        public IList<WebAssetResolverResult> Resolve()
        {
            var results = new List<WebAssetResolverResult>();

            foreach (var webAsset in webAssetGroup.Assets)
            {
                results.Add(ResolveWebAsset(webAssetGroup.Version, webAssetGroup.Compress, webAsset));
            }

            return results;
        }

        private WebAssetResolverResult ResolveWebAsset(string version, bool compress, IWebAsset webAsset)
        {
            var path = pathResolver.Resolve(DefaultSettings.GeneratedFilesPath, version, webAsset.Name);
            var assets = new List<IWebAsset>();
            assets.Add(webAsset);

            return new WebAssetResolverResult(path, compress, assets);
        }
    }
}
