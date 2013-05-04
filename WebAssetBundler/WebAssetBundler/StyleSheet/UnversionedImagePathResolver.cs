// Web Asset Bundler - Bundles web assets so you dont have to.
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
    using System.Collections.Generic;

    public class UnversionedImagePathResolver : IImagePathResolver
    {
        public PathRewriteResult Resolve(string path, string targetPath, string content)
        {
            var result = new PathRewriteResult();

            //ignore all absolute paths
            if (path.StartsWith("/") == false &&
                path.StartsWith("http", StringComparison.OrdinalIgnoreCase) == false &&
                path.StartsWith("https", StringComparison.OrdinalIgnoreCase) == false)
            {
                result.NewPath = RewritePath(path, targetPath);
                result.Changed = true;
            }

            return result;
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
