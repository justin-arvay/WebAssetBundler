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

    public class FromDirectoryAssetLocator : IAssetLocator<FromDirectoryComponent>
    {
        private HttpServerUtilityBase server;

        public FromDirectoryAssetLocator(HttpServerUtilityBase server)
        {
            this.server = server;
        }

        public ICollection<WebAsset> Locate(FromDirectoryComponent component)
        {
            var collecton = new List<WebAsset>();
            IEnumerable<string> fileNames = new List<string>(Directory.GetFiles(server.MapPath(component.Path)));

            if (IsFilteringRequired(component))
            {
                fileNames = FilterFiles(fileNames, component);
            }


            foreach (var fileName in fileNames)
            {
                collecton.Add(new WebAsset(fileName));
            }

            return collecton;
        }

        public bool IsFilteringRequired(FromDirectoryComponent component)
        {
            return component.StartsWithCollection.Count > 0 ||
                component.EndsWithCollection.Count > 0 ||
                component.ContainsCollection.Count > 0;
        }

        public IEnumerable<string> FilterFiles(IEnumerable<string> fileNames, FromDirectoryComponent component)
        {
            var filteredFileNames = fileNames.Where(
                    (name) => CompareAgainstCollection(component.StartsWithCollection, (x) => Path.GetFileNameWithoutExtension(name).StartsWith(x)) ||
                    CompareAgainstCollection(component.EndsWithCollection, (x) => Path.GetFileNameWithoutExtension(name).EndsWith(x)) ||
                    CompareAgainstCollection(component.ContainsCollection, (x) => Path.GetFileNameWithoutExtension(name).Contains(x))
            );

            return filteredFileNames;
        }

        public bool CompareAgainstCollection(ICollection<string> strings, Func<string, bool> callback)
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
