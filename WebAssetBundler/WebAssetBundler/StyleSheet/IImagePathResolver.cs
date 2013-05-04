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

    public interface IImagePathResolver
    {
        /// <summary>
        /// Changes the path and returns the difference as a result.
        /// </summary>
        /// <param name="path">The image path that is being changed</param>
        /// <param name="targetPath">The relative path that we need the original path to work with</param>
        /// <param name="filePath">The path of the file the image path was used in</param>
        /// <returns></returns>
        PathRewriteResult Resolve(string path, string targetPath, string filePath);
    }
}
