﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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
                content = pathResolverProvider.GetResolver(settings).Resolve(path, outputUrl, content);
            }
                
            asset.Content = content;
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
