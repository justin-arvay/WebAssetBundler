

namespace ResourceCompiler.Web.Mvc.Tests
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

            var assetElement = new AssetConfigurationElement()
            {
                Source = "~/"
            };

            var groupCollection = new GroupConfigurationElementCollection()
            {
                Combined = true,
                Compress = true,
                Version = "1.1"
            };

            groupCollection.Add(assetElement);

            section.Scripts.Add(groupCollection);
            section.StyleSheets.Add(groupCollection);

            return section;
        }
    }
}
