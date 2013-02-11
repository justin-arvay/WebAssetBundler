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
    using System.Linq;
    using System.Web;
    using System.Collections;

    public class AssetProvider : IAssetProvider
    {
        private HttpServerUtilityBase server;
        private string applicationPath;
        private string minifyIdentifier;
        private bool debugMode;

        public AssetProvider(HttpServerUtilityBase server, string applicationPath, Func<string> minifyIdentifier, Func<bool> debugMode)
        {
            this.server = server;
            this.applicationPath = applicationPath;
            this.minifyIdentifier = minifyIdentifier();
            this.debugMode = debugMode();
        }

        public AssetBase GetAsset(string source)
        {
            //user provided a minified source when in debug mode, try and change it to a non minifed version if it exists
            if (IsMinifedAsset(source))
            {
                if (debugMode)
                {
                    source = TryGetRawSource(source);
                }
            }

            //user provided a raw source, check if a minified version exists and use it instead if it does.
            else
            {
                source = TryGetMinifiedSource(source);
            }

            if (source.StartsWith("~/") == false && source.StartsWith("/") == false)
            {
                throw new ArgumentException("Source must be virtual path.");  
            }

            return new FileAsset(new AssetFile(source, server));
        }        

        public ICollection<AssetBase> GetAssets(FromDirectoryComponent component)
        {
            var collecton = new List<AssetBase>();
            var fullPath = server.MapPath(component.Path);

            IList<string> fileNames = new List<string>(Directory.GetFiles(fullPath)
                .Where(name => name.EndsWith(component.Extension)));

            //if the user has sepecidied additonal filtering, filter it
            if (IsFilteringRequired(component))
            {
                fileNames = FilterFiles(fileNames, component);
            }


            fileNames = RemoveDuplicates(fileNames);

            foreach (var fileName in fileNames)
            {
                //string  = fileName.Substring(applicationPath.Length);               
                var source = fileName.ReplaceFirst(applicationPath, "~/")
                            .Replace("\\", "/");
                collecton.Add(GetAsset(source));
            }

            return collecton;
        }

        /// <summary>
        /// Tries to get the minified source from a raw source. If the minified file does
        /// not exist the original source is returned.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string TryGetMinifiedSource(string source)
        {
            string minifedSource = GetMinifiedSource(source);

            if (File.Exists(server.MapPath(minifedSource)))
            {
                return minifedSource;
            }

            return source;
        }

        /// <summary>
        /// Tries to get the raw source from a source with minify identifier in it. If
        /// the raw source file does not exist, the original source is returned.
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string TryGetRawSource(string source)
        {
            var rawSource = GetRawSource(source);
            if (File.Exists(server.MapPath(rawSource)))
            {
                return rawSource;
            }

            return source;
        }

        /// <summary>
        /// Checks if the source is a minified asset.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsMinifedAsset(string source)
        {
            return Path.GetFileNameWithoutExtension(source).EndsWith(minifyIdentifier);
        }

        /// <summary>
        /// Changes the source to its raw version.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetRawSource(string source)
        {
            string ext = Path.GetExtension(source);
            return source.Substring(0, source.LastIndexOf(minifyIdentifier)) + ext;
        }

        /// <summary>
        /// Changes the source to its minifed version.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetMinifiedSource(string source)
        {
            string ext = Path.GetExtension(source);
            return source.Insert(source.LastIndexOf(ext), minifyIdentifier);
        }

        private IList<string> RemoveDuplicates(IList<string> fileNames)
        {
            var filteredFileNames = fileNames;

            for (int index = 0; index < fileNames.Count(); index++)
            {
                if (IsMinifedAsset(fileNames[index]))
                {
                    var rawSource = GetRawSource(fileNames[index]);
                    if (fileNames.Contains(rawSource))
                    {
                        filteredFileNames.Remove(fileNames[index]);
                    }
                }
            }

            return filteredFileNames;
        }

        private bool IsFilteringRequired(FromDirectoryComponent component)
        {
            return component.StartsWithCollection.Count > 0 ||
                component.EndsWithCollection.Count > 0 ||
                component.ContainsCollection.Count > 0;
        }

        private IList<string> FilterFiles(IList<string> fileNames, FromDirectoryComponent component)
        {
            var filteredFileNames = fileNames.Where(
                    (name) => CompareAgainstCollection(component.StartsWithCollection, (x) => Path.GetFileNameWithoutExtension(name).StartsWith(x)) ||
                    CompareAgainstCollection(component.EndsWithCollection, (x) => Path.GetFileNameWithoutExtension(name).EndsWith(x)) ||
                    CompareAgainstCollection(component.ContainsCollection, (x) => Path.GetFileNameWithoutExtension(name).Contains(x))
            );

            return filteredFileNames.ToList();
        }

        private bool CompareAgainstCollection(ICollection<string> strings, Func<string, bool> callback)
        {
            foreach (var str in strings)
            {
                if (callback(str))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
