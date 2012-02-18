using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ResourceCompiler.Utilities
{
    public class StyleSheetPathRewriter
    {

        public static string RewriteCssPaths(string outputPath, string sourcePath, string css)
        {
            var sourceUri = new Uri(Path.GetDirectoryName(sourcePath) + "/", UriKind.Absolute);
            var outputUri = new Uri(Path.GetDirectoryName(outputPath) + "/", UriKind.Absolute);

            var relativePaths = FindDistinctRelativePathsIn(css);

            foreach (string relativePath in relativePaths)
            {
                var resolvedSourcePath = new Uri(sourceUri + relativePath);
                var resolvedOutput = outputUri.MakeRelativeUri(resolvedSourcePath);

                css = css.Replace(relativePath, resolvedOutput.OriginalString);
            }
            return css;
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
