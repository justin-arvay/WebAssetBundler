
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Collections.Generic;
    using System.IO;

    public class ImagePathContentFilter : IWebAssetContentFilter
    {
        public ImagePathContentFilter()
        {            

        }

        public string Filter(string outputPath, string sourcePath, string content)
        {
            var sourceUri = new Uri(Path.GetDirectoryName(sourcePath) + "/", UriKind.Absolute);
            var outputUri = new Uri(Path.GetDirectoryName(outputPath) + "/", UriKind.Absolute);

            var relativePaths = FindDistinctRelativePathsIn(content);

            foreach (string relativePath in relativePaths)
            {
                var resolvedSourcePath = new Uri(sourceUri + relativePath);
                var resolvedOutput = outputUri.MakeRelativeUri(resolvedSourcePath);

                content = content.Replace(relativePath, resolvedOutput.OriginalString);
            }

            return content;
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
