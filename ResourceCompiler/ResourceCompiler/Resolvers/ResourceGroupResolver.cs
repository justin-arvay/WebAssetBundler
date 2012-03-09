using System;

namespace ResourceCompiler.Resolvers
{
    using System.Collections.Generic;
    using ResourceCompiler.Resource;
    using System.Linq;

    public class ResourceGroupResolver : IResourceResolver
    {
        private ResourceGroup resourceGroup;

        public ResourceGroupResolver(ResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        public IEnumerable<string> Resolve()
        {
            var relativePaths = new List<string>();

            foreach (var resource in resourceGroup.Resources)
            {
                relativePaths.Add(resource.Source);
            }

            return relativePaths;
        }
    }
}
