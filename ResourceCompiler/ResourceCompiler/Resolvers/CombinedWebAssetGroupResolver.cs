
namespace ResourceCompiler.Resolvers
{
    using System;
    using System.Collections.Generic;
    using ResourceCompiler.WebAsset;
    using System.Linq;

    public class CombinedWebAssetGroupResolver : IWebAssetResolver
    {
        private WebAssetGroup resourceGroup;

        public CombinedWebAssetGroupResolver(WebAssetGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        public IEnumerable<string> Resolve()
        {
            throw new NotImplementedException();
        }
    }
}
