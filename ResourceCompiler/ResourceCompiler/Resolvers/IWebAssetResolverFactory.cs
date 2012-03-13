
namespace ResourceCompiler.Resolvers
{
    using ResourceCompiler.WebAsset;

    public interface IWebAssetResolverFactory
    {
        IWebAssetResolver Create(WebAssetGroup resourceGroup);
    }
}
