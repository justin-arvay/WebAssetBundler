using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class WebAssetGroupBuilder
    {
        private readonly WebAssetGroup group;

        public WebAssetGroupBuilder(WebAssetGroup group)
        {
            this.group = group;
        }

        public WebAssetGroupBuilder Enable(bool value)
        {
            group.Enabled = value;
            return this;
        }

        public WebAssetGroupBuilder Version(string value)
        {
            group.Version = value;
            return this;
        }

        public WebAssetGroupBuilder Compress(bool value)
        {
            group.Compress = value;
            return this;
        }

        public WebAssetGroupBuilder Combine(bool value)
        {
            group.Combine = value;
            return this;
        }

        public WebAssetGroupBuilder Path(string path, Action<PathOnlyBuilder<WebAssetGroupBuilder>> builder)
        {
            return this;    
        }

        /// <summary>
        /// Adds a new resource to the group by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public WebAssetGroupBuilder Add(string filePath)
        {
            group.Assets.Add(new WebAsset(filePath));
            return this;
        }

    }
}
