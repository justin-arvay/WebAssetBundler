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
        private IImagePipelineRunner runner;
        private SettingsContext settings;
            
        public ImageProcessor(SettingsContext settings, IImagePipelineRunner runner)
        {
            this.runner = runner;
            this.settings = settings;
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
                    var context = CreateRunnerContext(path, asset);
                    var result = runner.Execute(context);

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

        public ImagePipelineRunnerContext CreateRunnerContext(string path, AssetBase asset)
        {
            return new ImagePipelineRunnerContext
            {
                ImagePath = path,
                SourcePath = asset.Source,
                AppRootDirectory = settings.AppRootDirectory
            };
        }
    }
}
