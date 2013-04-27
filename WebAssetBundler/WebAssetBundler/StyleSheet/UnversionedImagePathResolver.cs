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

    public class UnversionedImagePathResolver : IImagePathResolver
    {

        public string Resolve(string path, string targetPath, string content)
        {
            //ignore all absolute paths
            if (path.StartsWith("/") == false && path.StartsWith("http") == false && path.StartsWith("https") == false)
            {
                var newPath = RewritePath(path, targetPath);
                content = content.Replace(path, newPath);
            }

            return content;
        }

        private string RewritePath(string imagePath, string targetPath)
        {
            var levels = DirectoryLevelDifference(imagePath, targetPath);

            for (; levels > 0; levels--)
            {
                imagePath = "../" + imagePath;
            }

            return imagePath;
        }

        private int DirectoryLevelDifference(string imagePath, string targetPath)
        {
            var level = 0;
            string[] urlPieces = targetPath.Split('/');
            string[] imagePathPieces = imagePath.Split('/');

            foreach (var piece in urlPieces)
            {
                if (piece == "")
                {
                    continue;
                }

                level++;
            }

            foreach (var piece in imagePathPieces)
            {
                if (piece == "..")
                {
                    level--;
                }
            }

            return level;
        }
    }
}
