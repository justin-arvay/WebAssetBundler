namespace ResourceCompiler.Resource
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using ResourceCompiler.Extensions;

    public class ResourceGroupCollection : Collection<ResourceGroup>
    {

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public ResourceGroupCollection()
        {
        }

        /// <summary>
        /// Finds a resource group by name. If none is found returns null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ResourceGroup FindGroupByName(string name)
        {
            return Items.SingleOrDefault(g => g.Name.IsCaseInsensitiveEqual(name));
        }

    }
}
