using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResourceCompiler.Assets
{
    public class PathOnlyBuilder
    {
        IJavaScriptAssetsBuilder assetBuilder;

        string path;

        public PathOnlyBuilder(string path, IJavaScriptAssetsBuilder assetBuilder)
        {
            this.path = path;
            this.assetBuilder = assetBuilder;
        }

        public PathOnlyBuilder Add(string path)
        {
            if (Path.IsPathRooted(path))
            {
                throw new ArgumentException("Path must be a relative path.");
            }

            assetBuilder.Add(Path.Combine(this.path, path));

            return this;
        }
    }
}
