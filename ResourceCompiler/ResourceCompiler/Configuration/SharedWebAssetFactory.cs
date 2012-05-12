
namespace ResourceCompiler.Web.Mvc
{
    public class SharedWebAssetFactory : ISharedWebAssetFactory
    {
        public WebAsset Create(AssetConfigurationElement element)
        {
            return new WebAsset(element.Source);
        }
    }
}
