


namespace WebAssetBundler.Web.Mvc
{
    using System.Web.Configuration;

    public class AssetConfigurationFactory : IAssetConfigurationFactory
    {
        /// <summary>
        /// Creates a configuration section.
        /// </summary>
        /// <returns></returns>
        public SharedGroupConfigurationSection CreateSection()
        {
            return WebConfigurationManager.GetWebApplicationSection("bundler") as SharedGroupConfigurationSection;
        }

        /// <summary>
        /// Creates a web asset group from the configuration element.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public WebAssetGroup CreateGroup(GroupConfigurationElementCollection collection)
        {
            return new WebAssetGroup(collection.Name, true)
            {
                Combine = collection.Combine,
                Compress = collection.Compress
            };
        }

        /// <summary>
        /// Creates a web asset from a asset configuration element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public WebAsset CreateAsset(AssetConfigurationElement element)
        {
            return new WebAsset(element.Source);
        }
    }
}
