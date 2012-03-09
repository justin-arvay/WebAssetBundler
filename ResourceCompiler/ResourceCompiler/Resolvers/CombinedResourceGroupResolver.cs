
namespace ResourceCompiler.Resolvers
{
    using System;
    using System.Collections.Generic;
    using ResourceCompiler.Resource;
    using System.Linq;

    public class CombinedResourceGroupResolver : IResourceResolver
    {
        private ResourceGroup resourceGroup;

        public CombinedResourceGroupResolver(ResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        public IEnumerable<string> Resolve()
        {
            throw new NotImplementedException();
        }
    }
}
