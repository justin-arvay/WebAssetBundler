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

    public class ExpandPathProcessor : IPipelineProcessor<StyleSheetBundle>, IAssetModifier
    {
        private string outputUrl;
        private SettingsContext settings;
        private IImagePathResolverProvider pathResolverProvider;

        public ExpandPathProcessor(SettingsContext settings, IImagePathResolverProvider pathResolverProvider)
        {
            this.settings = settings;
            this.pathResolverProvider = pathResolverProvider;
        }

        public void Process(StyleSheetBundle bundle)
        {
            outputUrl = bundle.Url;
            bundle.Assets.AddModifier(this);      
        }

        // source: ~/Content/file.css
        // image: ../image/icon.png
        //target: /wab.axd/a/a
        public Stream Modify(Stream openStream, AssetBase asset)        
        {
            var content = openStream.ReadToEnd();
            var paths = FindPaths(content);

            foreach (string path in paths)
            {
                var result = pathResolverProvider.GetResolver(settings).Resolve(path, outputUrl, asset.Source);

                if (result.Changed)
                {
                    content = content.Replace(path, result.NewPath);
                }
            }

            return content.ToStream();
        }         
        
        private IEnumerable<string> FindPaths(string css)
        {
            var matchesHash = new HashSet<string>();
            var urlMatches = Regex.Matches(css, @"url\s*\(\s*[""']{0,1}(.+?)[""']{0,1}\s*\)", RegexOptions.IgnoreCase);

            foreach (Match match in urlMatches)
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
