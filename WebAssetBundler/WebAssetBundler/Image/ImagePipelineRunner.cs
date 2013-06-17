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

    public class ImagePipelineRunner : IImagePipelineRunner
    {
        private IBundlePipeline<ImageBundle> pipeline;
        private BundleCache<ImageBundle> bundlesCache;
        private IBundleFactory<ImageBundle> bundleFactory;

        public ImagePipelineRunner(IBundlePipeline<ImageBundle> pipeline, BundleCache<ImageBundle> bundlesCache,
            IBundleFactory<ImageBundle> bundleFactory)
        {
            this.pipeline = pipeline;
            this.bundlesCache = bundlesCache;
            this.bundleFactory = bundleFactory;
        }

        public ImagePipelineRunnerResult Execute(ImagePipelineRunnerContext context)
        {
            var result = new ImagePipelineRunnerResult
            {
                OldPath = context.ImagePath
            };

            //ignore external paths, we cannot deal with these (yet)
            if (context.ImagePath.ToLower().StartsWith("http", StringComparison.OrdinalIgnoreCase) == false &&
                context.ImagePath.ToLower().StartsWith("https", StringComparison.OrdinalIgnoreCase) == false)
            {

                //one bundle and one asset is created for each image
                //bundles are processed and cached like other bundles

                ImageBundle bundle = CreateImageBundle(context);                
                pipeline.Process(bundle);
                bundlesCache.Add(bundle);

                result.NewPath = bundle.Url;
                result.Changed = true;
            }

            return result;
        }

        public AssetBase GetAsset(ImagePipelineRunnerContext context)
        {
            //test for: ../Image/img.png and /Image/image.png
            string directoryName = Path.GetDirectoryName(context.SourcePath);
            IDirectory directory = context.AppRootDirectory.GetDirectory(directoryName);
            IFile file = directory.GetFile(context.ImagePath);

            return new FileSystemAsset(file);
        }

        public ImageBundle CreateImageBundle(ImagePipelineRunnerContext context)
        {
            AssetBase asset = GetAsset(context);
            return bundleFactory.Create(asset);
        }
    }
}
