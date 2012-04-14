namespace ResourceCompiler.Web.Mvc
{
    using System.Linq;
    using ResourceCompiler.Web.Mvc;

    public class WebAssetResolverFactory : IWebAssetResolverFactory
    {
        private IPathResolver pathResolver;

        public WebAssetResolverFactory(IPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }

        public IWebAssetResolver Create(WebAssetGroup webAssetGroup)
        {
            if (webAssetGroup.Combine)
            {
                return new CombinedWebAssetGroupResolver(webAssetGroup, pathResolver);
            }

            if (webAssetGroup.Version.IsNotNullOrEmpty())
            {
                return new VersionedWebAssetGroupResolver(webAssetGroup, pathResolver);
            }

            return new WebAssetGroupResolver(webAssetGroup);
        }
    }
}
