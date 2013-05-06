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
    using System.IO;

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
            var reader = new BackgroundImageReader();

            //go through each asset to read the image paths
            //we need to run the pipeline for each image
            foreach (var asset in bundle.Assets)
            {
                var results = new List<ImagePipelineRunnerResult>();
                var paths = reader.ReadAll(asset.Content);

                foreach (var path in paths)
                {
                    //this is where we execute the image pipeline
                    var result = ProcessImage(path, asset.Source);

                    if (result.Changed)
                    {
                        //results for he modifier to change the css file to whatever new url was generated in the pipeline
                        results.Add(result);
                    }
                }

                if (results.Count > 0)
                {
                    //a separate instance of the modifier for each instance since the results are unique to each asset
                    asset.Modifiers.Add(new BackgroundImageModifier(results));
                }
            }
        }

        public ImagePipelineRunnerResult ProcessImage(string path, string filePath)
        {
            var result = new ImagePipelineRunnerResult
            {
                OldPath = path
            };

            //ignore external paths, we cannot deal with these (yet)
            if (path.ToLower().StartsWith("http", StringComparison.OrdinalIgnoreCase) == false &&
                path.ToLower().StartsWith("https", StringComparison.OrdinalIgnoreCase) == false)
            {

                //one bundle and one asset is created for each image
                //bundles are processed and cached like other bundles
                var bundle = CreateImageBundle(path, filePath);                
                pipeline.Process(bundle);
                bundlesCache.Add(bundle);

                result.NewPath = bundle.Url;
                result.Changed = true;
            }

            return result;
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

            return bundle;
        }
    }
}
