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
    using System.Text.RegularExpressions;

    public class DirectorySearch : IDirectorySearch
    {
        public DirectorySearch()
        {
            SearchOption = SearchOption.AllDirectories;
            Patterns = new List<string>();
            OrderPatterns = new List<string>();
        }

        public ICollection<string> Patterns
        { 
            get; 
            set; 
        }

        public SearchOption SearchOption 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// A list of different patterns that determine how files are ordered when being returned by Search
        /// </summary>
        public IList<string> OrderPatterns
        {
            get;
            set;
        }

        public IEnumerable<IFile> Search(IDirectory directory)
        {
            IEnumerable<IFile> files =
                from pattern in Patterns
                from file in directory.GetFiles(pattern, SearchOption)
                select file;
            
            return OrderFiles(files.Distinct(new FileSystemFileComparer()), directory.FullPath);
        }

        /// <summary>
        /// Orders files by the order patterns in a LILO form. That is if two patterns match the same
        /// file, the last most pattern will determine where to position the file in the returned collection.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public IEnumerable<IFile> OrderFiles(IEnumerable<IFile> files, string dirPath)
        {
            var matches = new List<IFile>();

            foreach (var pattern in OrderPatterns)
            {
                if (IsPatternValid(pattern) == false)
                {
                    throw new InvalidDataException(TextResource.Exceptions.InvalidDirectorySearchOrderPattern.FormatWith(pattern));
                }

                var fullPattern = PathHelper.NormalizePath(Path.Combine(dirPath, pattern));
                matches.AddRange(files.Where((file) => IsMatch(fullPattern, file)));
            }

            //distinct checks from the start and removes matches at the end which destroys
            // and wildcard ordering so by reversing it first we ensure the order is preserved
            // while also removing all the duplicates at the begining. 
            matches.Reverse();
            matches = matches.Distinct(new FileSystemFileComparer()).ToList();
            matches.Reverse();

            //gather the remaining files that where not matches for ordering
            var remaining = (from f in files
                             where NotIn(f, matches)
                             select f).ToList();

            matches.AddRange(remaining);

            return matches;
        }

        private bool NotIn(IFile file, IEnumerable<IFile> files)
        {
            return (from f in files select f)
                .Contains(file, new FileSystemFileComparer()) == false;
        }

        private bool IsMatch(string pattern, IFile file)
        {
            var wildcard = new Wildcard(pattern);
            
            return wildcard.IsMatch(file.Path);   
        }

        /// <summary>
        /// Checks if the pattern is considered valid. Must be a relative path to the directory we are searching through. Cannot use: (~) or (/) or (..)
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private bool IsPatternValid(string pattern)
        {
            if (pattern.StartsWith("~") || pattern.StartsWith("/") || pattern.StartsWith(".."))
            {
                return false;
            }

            return true;
        }        
    }
}
