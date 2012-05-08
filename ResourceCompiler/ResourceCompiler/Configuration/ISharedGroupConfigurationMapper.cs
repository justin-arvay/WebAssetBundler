
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISharedGroupConfigurationMapper
    {
        public void MapStyleSheets(IList<WebAssetGroup> groups);
        public void MapScripts(IList<WebAssetGroup> groups);
    }
}
