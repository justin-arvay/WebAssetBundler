

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SharedGroupConfigurationLoader : ISharedGroupConfigurationLoader
    {
        private SharedGroupConfigurationSection section;
        private ISharedWebAssetGroupFactory groupFactory;
        private ISharedWebAssetFactory assetFactory;

        public SharedGroupConfigurationLoader(IConfigurationSectionFactory sectionFactory, 
            ISharedWebAssetGroupFactory groupFactory, 
            ISharedWebAssetFactory assetFactory)
        {
            this.section = sectionFactory.Create();
            this.groupFactory = groupFactory;
            this.assetFactory = assetFactory;
        }

        public void LoadStyleSheets(IList<WebAssetGroup> groups)
        {
            if (section != null)
            {
                foreach (GroupConfigurationElementCollection group in section.StyleSheets)
                {
                    var webAssetGroup = groupFactory.Create(group);

                    foreach (AssetConfigurationElement asset in group)
                    {
                        webAssetGroup.Assets.Add(assetFactory.Create(asset));
                    }

                    groups.Add(webAssetGroup);
                }
            }
        }

        public void LoadScripts(IList<WebAssetGroup> groups)
        {
            if (section != null)
            {
                foreach (GroupConfigurationElementCollection group in section.Scripts)
                {
                    var webAssetGroup = groupFactory.Create(group);

                    foreach (AssetConfigurationElement asset in group)
                    {
                        webAssetGroup.Assets.Add(assetFactory.Create(asset));
                    }

                    groups.Add(webAssetGroup);
                }
            }
        }
    }
}
