

namespace WebAssetBundler.Web.Mvc
{
    public interface IAssetConfigurationFactory
    {
        SharedGroupConfigurationSection CreateSection();
        WebAssetGroup CreateGroup(GroupConfigurationElementCollection collection);
        WebAsset CreateAsset(AssetConfigurationElement element);
    }
}
