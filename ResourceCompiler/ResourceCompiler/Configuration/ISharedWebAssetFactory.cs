
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISharedWebAssetFactory
    {
        WebAsset Create(AssetConfigurationElement element);
    }
}
