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

        public IWebAssetResolver Create(WebAssetGroup resourceGroup)
        {
            if (resourceGroup.Combined)
            {
                return new CombinedWebAssetGroupResolver(resourceGroup, pathResolver);
            }

            return new WebAssetGroupResolver(resourceGroup);
        }
    }
}
