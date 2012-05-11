
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISharedWebAssetGroupFactory
    {
        WebAssetGroup Create(GroupConfigurationElementCollection collection);
    }
}
