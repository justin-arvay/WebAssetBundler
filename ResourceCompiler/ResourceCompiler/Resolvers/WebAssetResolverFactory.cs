namespace ResourceCompiler.Resolvers
{
    using System.Linq;
    using ResourceCompiler.WebAsset;

    public class WebAssetResolverFactory : IWebAssetResolverFactory
    {

        public IWebAssetResolver Create(WebAssetGroup resourceGroup)
        {
            if (resourceGroup.Combined)
            {
                return new CombinedWebAssetGroupResolver(resourceGroup);
            }

            return new WebAssetGroupResolver(resourceGroup);
        }
    }
}
