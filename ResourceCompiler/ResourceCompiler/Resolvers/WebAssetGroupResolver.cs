

namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
    using System.Linq;

    public class WebAssetGroupResolver : IWebAssetResolver
    {
        private WebAssetGroup webAssetGroup;
        private IPathResolver pathResolver;

        public WebAssetGroupResolver(WebAssetGroup webAssetGroup)
        {
            this.webAssetGroup = webAssetGroup;            
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
            var assets = new List<IWebAsset>();
            assets.Add(webAsset);

            return new WebAssetResolverResult(webAsset.Source, assets);
        }
    }
}
