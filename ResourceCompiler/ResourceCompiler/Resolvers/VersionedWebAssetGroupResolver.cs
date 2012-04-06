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

        public ICollection<WebAssetResolverResult> Resolve()
        {
            var results = new List<WebAssetResolverResult>();

            foreach (var webAsset in webAssetGroup.Assets)
            {
                results.Add(ResolveWebAsset(webAssetGroup.Name, webAssetGroup.Version, webAsset));
            }

            return results;
        }

        private WebAssetResolverResult ResolveWebAsset(string name, string version, IWebAsset webAsset)
        {
            var path = pathResolver.Resolve(DefaultSettings.GeneratedFilesPath, version, name, webAsset.Extension);
            var assets = new List<IWebAsset>();
            assets.Add(webAsset);

            return new WebAssetResolverResult(path, assets);
        }
    }
}
