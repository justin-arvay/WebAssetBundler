﻿// Web Asset Bundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.IO;

    public class ExpandPathProcessor : IPipelineProcessor<StyleSheetBundle>, IAssetModifier
    {
        private string outputUrl;
        //private SettingsContext settings;

        public ExpandPathProcessor(SettingsContext settings)
        {
            //this.settings = settings;
        }

        public void Process(StyleSheetBundle bundle)
        {
            outputUrl = bundle.Url;
            bundle.Assets.Modify(this);
        }

        public Stream Modify(Stream openStream)
        {
            var reader = new BackgroundImageReader();            
            var content = openStream.ReadToEnd();
            var paths = reader.ReadAll(content);

            foreach (var path in paths)
            {
                //ignore all absolute paths
                if (path.StartsWith("/") == false &&
                    path.StartsWith("http", StringComparison.OrdinalIgnoreCase) == false &&
                    path.StartsWith("https", StringComparison.OrdinalIgnoreCase) == false)
                {
                    var newPath = RewritePath(path, outputUrl);
                    content = content.Replace(path, newPath);
                }
            }

            return content.ToStream();            
        }

        private string RewritePath(string imagePath, string targetPath)
        {
            imagePath = GetDirectoryLevelDifference(imagePath, targetPath) + imagePath;

            return imagePath;
        }

        private string GetDirectoryLevelDifference(string imagePath, string targetPath)
        {
            var stack = new Stack<string>();
            string[] urlPieces = targetPath.Split('/');
            string[] imagePathPieces = imagePath.Split('/');

            foreach (var piece in urlPieces)
            {
                if (piece == "")
                {
                    continue;
                }

                stack.Push("..");
            }

            foreach (var piece in imagePathPieces)
            {
                if (piece == "..")
                {
                    stack.Pop();
                }
            }

            return string.Join("/", stack.ToArray()) + "/";
        }
    }
}
