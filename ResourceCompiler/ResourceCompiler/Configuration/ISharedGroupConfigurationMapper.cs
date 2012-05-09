
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISharedGroupConfigurationMapper
    {
        void MapStyleSheets(IList<WebAssetGroup> groups);
        void MapScripts(IList<WebAssetGroup> groups);
    }
}
