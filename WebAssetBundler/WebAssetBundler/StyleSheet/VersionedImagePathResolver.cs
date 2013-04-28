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

    public class VersionedImagePathResolver : IImagePathResolver
    {
        private IBundlesCache<ImageBundle> bundlesCache;
        private IUrlGenerator<ImageBundle> urlGenerator;
        private SettingsContext settings;

        public VersionedImagePathResolver(SettingsContext settings, IBundlesCache<ImageBundle> bundlesCache, 
            IUrlGenerator<ImageBundle> urlGenerator)
        {
            this.settings = settings;
            this.urlGenerator = urlGenerator;
            this.bundlesCache = bundlesCache;
        }

        public PathRewriteResult Resolve(string path, string targetPath, string filePath)
        {
            var result = new PathRewriteResult();

            //ignore external paths, we cannot version those
            if (path.StartsWith("http") == false)
            {
                var bundle = CreateImageBundle(path, filePath);
                bundlesCache.Add(bundle);

                result.NewPath = urlGenerator.Generate(bundle);
                result.Changed = true;
            }

            return result;
        }

        public AssetBase GetAsset(string imagePath, string cssFilePath)
        {
            //test for: ../Image/img.png and /Image/image.png

            var directory = settings.AppRootDirectory.GetDirectory(cssFilePath);
            var file = directory.GetFile(imagePath);

            return new FileAsset(file);
        }

        public ImageBundle CreateImageBundle(string path, string cssFilePath)
        {
            var contentType = GetContentType(path);

            var bundle = new ImageBundle(contentType, path);
            bundle.Assets.Add(GetAsset(path, cssFilePath));

            return bundle;
        }

        /// <summary>
        /// Gets the content type.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetContentType(string path)
        {
            switch (Path.GetExtension(path).ToLower())
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
                    return "image/x-png";
                case "bmp":
                    return "image/x-ms-bmp";

            }

            return "";
        }
    }
}
