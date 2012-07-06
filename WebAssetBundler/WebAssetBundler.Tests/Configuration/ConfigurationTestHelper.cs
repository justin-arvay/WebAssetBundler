

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System.Configuration;

    public static class ConfigurationTestHelper
    {


        public static SharedGroupConfigurationSection CreateSection()
        {
            var section = new SharedGroupConfigurationSection()
            {
                StyleSheets = new StyleSheetConfigurationElementCollection(),
                Scripts = new ScriptConfigurationElementCollection()
            };            

            var collectionOne = new GroupConfigurationElementCollection()
            {
                Combine = true,
                Compress = true,
                Version = "1.1"
            };

            var collectionTwo = new GroupConfigurationElementCollection()
            {
                Combine = true,
                Compress = true,
                Version = "1.1"
            };

            collectionOne.Add(new AssetConfigurationElement()
            {
                Source = "~/AssetOne"
            });

            collectionTwo.Add(new AssetConfigurationElement()
            {
                Source = "~/AssetTwo"
            });

            section.Scripts.Add(collectionOne);
            section.StyleSheets.Add(collectionTwo);

            return section;
        }
    }
}
