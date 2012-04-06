
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using ResourceCompiler.Web.Mvc;
    using System.Linq;

    public class CombinedWebAssetGroupResolver : IWebAssetResolver
    {
        private WebAssetGroup resourceGroup;
        private IPathResolver pathResolver;

        public CombinedWebAssetGroupResolver(WebAssetGroup resourceGroup, IPathResolver pathResolver)
        {
            this.resourceGroup = resourceGroup;
            this.pathResolver = pathResolver;
        }

        public IEnumerable<string> Resolve()
        {
            var basePath = GetBasePath();

            var paths = new List<string>();

            paths.Add(pathResolver.Resolve("", resourceGroup.Version, resourceGroup.Name, 
        }

        private string GetBasePath()
        {
            var basePath = DefaultSettings.GeneratedFilesPath;

            if (basePath.StartsWith("~"))
            {
                basePath = basePath.TrimStart('~');
            }

            return basePath;
        }
    }
}
