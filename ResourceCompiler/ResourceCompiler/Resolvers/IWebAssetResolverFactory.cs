
namespace ResourceCompiler.Web.Mvc
{
    using ResourceCompiler.Web.Mvc;

    public interface IWebAssetResolverFactory
    {
        IWebAssetResolver Create(WebAssetGroup resourceGroup);
    }
}
