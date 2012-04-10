
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Collections.Generic;
    using System.IO;

    public class ImagePathContentFilter : IWebAssetContentFilter
    {


        //private static readonly Regex urlRegex = new Regex(@"url\s*\((\""|\')?(?<url>[^)]+)?(\""|\')?\)",
            //RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

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

        private static IEnumerable<string> FindDistinctRelativePathsIn(string css)
        {
            var matchesHash = new HashSet<string>();
            var matches = Regex.Matches(css, @"url\([""']{0,1}(.+?)[""']{0,1}\)", RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                var path = match.Groups[1].Captures[0].Value;

                //remove the starting slash if it exists
                if (path.StartsWith("/"))
                {
                    path = path.Substring(1);
                }

                //if the path does not already exist, add it.
                if (matchesHash.Add(path))
                {
                    yield return path;
                }
            }
        }
    }
}
