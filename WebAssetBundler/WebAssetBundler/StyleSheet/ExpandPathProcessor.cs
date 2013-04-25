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
    using System.Web;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.IO;

    public class ExpandPathProcessor : IPipelineProcessor<StyleSheetBundle>, IAssetTransformer
    {
        private string outputUrl;
        private IBundlesCache<ImageBundle> bundleCache;
        private IUrlGenerator<ImageBundle> urlGenerator;
        private SettingsContext settings;

        public ExpandPathProcessor(SettingsContext settings, IUrlGenerator<ImageBundle> urlGenerator, 
            IBundlesCache<ImageBundle> bundleCache)
        {
            this.urlGenerator = urlGenerator;
            this.bundleCache = bundleCache;
            this.settings = settings;
        }

        public void Process(StyleSheetBundle bundle)
        {
            outputUrl = bundle.Url;
            bundle.TransformAssets(this);            
        }

        // source: ~/Content/file.css
        // image: ../image/icon.png
        //target: /wab.axd/a/a
        public void Transform(AssetBase asset)
        {            
            var content = asset.Content;
            var paths = FindPaths(content);

            foreach (string path in paths)
            {
                if (settings.VersionCssImages)
                {
                    content = TransformToVersionedImage(path, content);     
                }
                else
                {
                    content = TransformToUncachedImage(path, content);     
                }                 
            }
                
            asset.Content = content;
        }

        public string TransformToUncachedImage(string path, string content)
        {
            //ignore all absolute paths
            if (path.StartsWith("/") == false && path.StartsWith("http") == false && path.StartsWith("https") == false)
            {
                var newPath = RewritePath(path);
                content = content.Replace(path, newPath);
            }

            return content;
        }

        public string TransformToVersionedImage(string path, string content)
        {
            //ignore external paths, we cannot version those
            if (path.StartsWith("http") == false)
            {
                var bundle = CreateImageBundle(path);
                bundleCache.Add(bundle);

                content = content.Replace(path, urlGenerator.Generate(bundle));
            }

            return content;
        }

        public ImageBundle CreateImageBundle(string path)
        {
            var contentType = GetContentType(path);
            var asset = new FileAsset(new FileSystemFile(path));
            
            var bundle = new ImageBundle(contentType, path);
            bundle.Assets.Add(asset);

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


        private string RewritePath(string imagePath)
        {
            var levels = DirectoryLevelDifference(imagePath);

            for (; levels > 0; levels--)
            {
                imagePath = "../" + imagePath;
            }

            return imagePath;
        }

        private int DirectoryLevelDifference(string imagePath)
        {
            var level = 0;
            string[] urlPieces = outputUrl.Split('/');
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
        
        private IEnumerable<string> FindPaths(string css)
        {
            var matchesHash = new HashSet<string>();
            var urlMatches = Regex.Matches(css, @"url\([""']{0,1}(.+?)[""']{0,1}\)", RegexOptions.IgnoreCase);
            var srcMatches = Regex.Matches(css, @"\(src\=[""']{0,1}(.+?)[""']{0,1}\)", RegexOptions.IgnoreCase);

            foreach (Match match in urlMatches)
            {
                matchesHash.Add(GetUrlFromMatch(match));
            }

            foreach (Match match in srcMatches)
            {
                matchesHash.Add(GetUrlFromMatch(match));
            }

            return matchesHash;
        }

        private string GetUrlFromMatch(Match match)
        {
            return match.Groups[1].Captures[0].Value;
        }
         
    }
}
