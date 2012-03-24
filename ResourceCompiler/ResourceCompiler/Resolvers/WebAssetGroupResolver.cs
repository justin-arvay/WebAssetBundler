using System;

namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
    using System.Linq;

    public class WebAssetGroupResolver : IWebAssetResolver
    {
        private WebAssetGroup resourceGroup;

        public WebAssetGroupResolver(WebAssetGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        public IEnumerable<string> Resolve()
        {
            var relativePaths = new List<string>();

            foreach (var resource in resourceGroup.Assets)
            {
                relativePaths.Add(resource.Source);
            }

            return relativePaths;
        }
    }
}
