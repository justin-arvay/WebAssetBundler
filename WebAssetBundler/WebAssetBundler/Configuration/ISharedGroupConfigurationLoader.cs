
namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISharedGroupConfigurationLoader
    {
        void LoadStyleSheets(IList<WebAssetGroup> groups);
        void LoadScripts(IList<WebAssetGroup> groups);
    }
}
