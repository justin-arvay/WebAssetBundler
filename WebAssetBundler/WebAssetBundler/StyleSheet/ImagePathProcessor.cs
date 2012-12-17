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

    public class ImagePathProcessor : IPipelineProcessor<StyleSheetBundle>, IAssetTransformer
    {
        private HttpServerUtilityBase server;
        private BundleContext context;

        public ImagePathProcessor(IUrlGenerator<StyleSheetBundle> urlGenerator, HttpServerUtilityBase server, BundleContext context)
        {
            this.server = server;
            this.context = context;
        }

        public void Process(StyleSheetBundle bundle)
        {
            bundle.TransformAssets(this);
        }

        public void Transform(AssetBase asset)
        {
            var outputUrl = outputUrl = urlGenerator.Generate("a", "a", "", context)        
            var content = asset.Content;

            var sourceUri = new Uri(Path.GetDirectoryName(server.MapPath(asset.Source)), UriKind.Absolute);
            var outputUri = new Uri(Path.GetDirectoryName(outputUrl) + "/", UriKind.Absolute);

            var relativePaths = FindDistinctRelativePathsIn(content);

            foreach (string relativePath in relativePaths)
            {
                var resolvedSourcePath = new Uri(sourceUri + relativePath);
                var resolvedOutput = outputUri.MakeRelativeUri(resolvedSourcePath);

                content = content.Replace(relativePath, resolvedOutput.OriginalString);
            }

            asset.Content = content;
        }

        private IEnumerable<string> FindDistinctRelativePathsIn(string css)
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
            var path = match.Groups[1].Captures[0].Value;

            //remove the starting slash if it exists
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            return path;
        }
    }
}
