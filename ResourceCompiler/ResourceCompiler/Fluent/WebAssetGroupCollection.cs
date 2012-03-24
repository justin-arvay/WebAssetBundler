namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;

    public class WebAssetGroupCollection : Collection<WebAssetGroup>
    {

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public WebAssetGroupCollection()
        {
        }

        /// <summary>
        /// Finds a resource group by name. If none is found returns null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public WebAssetGroup FindGroupByName(string name)
        {
            return Items.SingleOrDefault(g => g.Name.IsCaseInsensitiveEqual(name));
        }

    }
}
