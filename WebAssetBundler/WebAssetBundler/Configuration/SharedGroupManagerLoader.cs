

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SharedGroupManagerLoader : ISharedGroupManagerLoader
    {
        private IAssetConfigurationFactory configFactory;

        public SharedGroupManagerLoader(IAssetConfigurationFactory configFactory)
        {
            this.configFactory = configFactory;           
        }

        public void Load(SharedGroupManager manager)
        {
            LoadStyleSheets(manager.StyleSheets);
            LoadScripts(manager.Scripts);
        }

        private void LoadStyleSheets(IList<WebAssetGroup> groups)
        {
            var section = configFactory.CreateSection();

            if (section != null)
            {
                foreach (GroupConfigurationElementCollection group in section.StyleSheets)
                {
                    var webAssetGroup = configFactory.CreateGroup(group);

                    foreach (AssetConfigurationElement asset in group)
                    {
                        webAssetGroup.Assets.Add(configFactory.CreateAsset(asset));
                    }

                    groups.Add(webAssetGroup);
                }
            }
        }

        private void LoadScripts(IList<WebAssetGroup> groups)
        {
            var section = configFactory.CreateSection();

            if (section != null)
            {
                foreach (GroupConfigurationElementCollection group in section.Scripts)
                {
                    var webAssetGroup = configFactory.CreateGroup(group);

                    foreach (AssetConfigurationElement asset in group)
                    {
                        webAssetGroup.Assets.Add(configFactory.CreateAsset(asset));
                    }

                    groups.Add(webAssetGroup);
                }
            }
        }
        
    }
}
