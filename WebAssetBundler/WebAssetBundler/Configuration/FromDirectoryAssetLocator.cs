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
            var directoryInfo = new DirectoryInfo(server.MapPath(component.Path));

            var fileInfos = directoryInfo.GetFiles()
                .Where(
                    (fileInfo) => CompareAgainstCollection(component.StartsWithCollection, (x) => fileInfo.Name.StartsWith(x)) ||
                    CompareAgainstCollection(component.EndsWithCollection, (x) => fileInfo.Name.EndsWith(x)) ||
                    CompareAgainstCollection(component.ContainsCollection, (x) => fileInfo.Name.Contains(x))
            );

            foreach (var fileInfo in fileInfos)
            {
                collecton.Add(new WebAsset(fileInfo.FullName));
            }

            return collecton;
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
