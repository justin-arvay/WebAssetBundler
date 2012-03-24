﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResourceCompiler.Web.Mvc
{
    public class PathOnlyBuilder<TBuilder>
        where TBuilder : WebAssetGroupBuilder
    {
        TBuilder assetBuilder;

        string path;

        public PathOnlyBuilder(string path, TBuilder assetBuilder)
        {
            this.path = path;
            this.assetBuilder = assetBuilder;
        }

        public PathOnlyBuilder<TBuilder> Add(string path)
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