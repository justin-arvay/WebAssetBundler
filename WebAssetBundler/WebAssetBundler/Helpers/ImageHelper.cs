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
    using System.IO;
    using System.Drawing;

    public static class ImageHelper
    {
        /// <summary>
        /// Gets the content type.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetContentType(string path)
        {
            string ext = Path.GetExtension(path).ToLower().Replace(".", String.Empty);

            switch (ext)
            {
                case "gif":
                    return "image/gif";
                case "jpeg":
                case "jpg":
                case "jpe":
                    return "image/jpeg";
                case "tiff":
                case "tif":
                    return "image/tiff";
                case "png":
                    return "image/png";
                case "bmp":
                    return "image/bmp";

            }

            return "";
        }

        /// <summary>
        /// Gets the
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static SizeF GetDimensions(IFile file)
        {
            using (var stream = file.Open(FileMode.Open, FileAccess.Read))
            using (var image = new Bitmap(stream))
            {
                return image.PhysicalDimension;
            }
        }

        /// <summary>
        /// Creates a bundle name from a relative url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CreateBundleName(string url)
        {
            string directoryName = Path.GetDirectoryName(url);
            string fileName = Path.GetFileName(url);

            return directoryName.ToHash() + "-" + fileName.Replace('.', '-');
        }
    }
}
