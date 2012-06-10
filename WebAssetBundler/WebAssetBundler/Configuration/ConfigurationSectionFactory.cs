
namespace WebAssetBundler.Web.Mvc
{
    using System.Web.Configuration;

    public class ConfigurationSectionFactory : IConfigurationSectionFactory
    {
        public SharedGroupConfigurationSection Create()
        {
            return WebConfigurationManager.GetWebApplicationSection("bundler") as SharedGroupConfigurationSection;
        }
    }
}
