﻿namespace ResourceCompiler.Web.Mvc
{
    using System.Linq;
    using ResourceCompiler.Web.Mvc;

    public class WebAssetResolverFactory : IWebAssetResolverFactory
    {

        public IWebAssetResolver Create(WebAssetGroup resourceGroup)
        {
            if (resourceGroup.Combined)
            {
                return new CombinedWebAssetGroupResolver(resourceGroup);
            }

            return new WebAssetGroupResolver(resourceGroup);
        }
    }
}
