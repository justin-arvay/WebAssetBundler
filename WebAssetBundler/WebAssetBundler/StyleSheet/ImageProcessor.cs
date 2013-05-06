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

    public class ImageProcessor : IPipelineProcessor<StyleSheetBundle>
    {
        private IBundlePipeline<ImageBundle> pipeline;
        private SettingsContext settings;
        private IBundlesCache<ImageBundle> bundlesCache;

        public ImageProcessor(IBundlePipeline<ImageBundle> pipeline, IBundlesCache<ImageBundle> bundlesCache, SettingsContext settings)
        {
            this.pipeline = pipeline;
            this.settings = settings;
            this.bundlesCache = bundlesCache;
        }

        public void Process(StyleSheetBundle bundle)
        {
            //pipeline.Process(bundle);
            //TODO: move the versioned image path resolver code into here
        }

        public void ProcessImage(string path, string targetPath, string filePath)
        {
            //ignore external paths, we cannot deal with these (yet)
            if (path.ToLower().StartsWith("http", StringComparison.OrdinalIgnoreCase) == false &&
                path.ToLower().StartsWith("https", StringComparison.OrdinalIgnoreCase) == false)
            {
                var bundle = CreateImageBundle(path, filePath);
                bundlesCache.Add(bundle);

                pipeline.Process(bundle);
            }
        }

        public AssetBase GetAsset(string imagePath, string cssFilePath)
        {
            //test for: ../Image/img.png and /Image/image.png
            var directoryName = Path.GetDirectoryName(cssFilePath);
            var directory = settings.AppRootDirectory.GetDirectory(directoryName);
            var file = directory.GetFile(imagePath);

            return new FileAsset(file);
        }

        public ImageBundle CreateImageBundle(string path, string cssFilePath)
        {
            var contentType = ImageHelper.GetContentType(path);

            var bundle = new ImageBundle(contentType, path);
            bundle.Assets.Add(GetAsset(path, cssFilePath));

            var hashProcessor = new AssignHashProcessor();
            hashProcessor.Process(bundle);

            return bundle;
        }
    }
}
