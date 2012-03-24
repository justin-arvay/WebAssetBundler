
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
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
