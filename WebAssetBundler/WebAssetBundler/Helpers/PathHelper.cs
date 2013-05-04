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
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    public static class PathHelper
    {
        public static string NormalizePath(string path)
        {
            var isNetworkShare = path.StartsWith(@"\\");
            var stack = new Stack<string>();
            var slashes = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            var parts = path.Split(slashes, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                }
                else if (part != ".")
                {
                    stack.Push(part);
                }
            }

            if (isNetworkShare)
            {
                return @"\\" + string.Join(@"\", stack.Reverse().ToArray());
            }

            var returnPath = string.Join("/", stack.Reverse().ToArray());

            //keep forward slash if it existed in original path
            if (path[0] == '/')
            {
                returnPath = "/" + returnPath;
            }

            return returnPath;
        }
    }
}
