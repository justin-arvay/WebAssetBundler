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

    public class AssetProvider : IAssetProvider
    {
        private HttpServerUtilityBase server;
        private string applicationPath;
        private BundleContext context;

        public AssetProvider(HttpServerUtilityBase server, string applicationPath, BundleContext context)
        {
            this.context = context;
            this.server = server;
            this.applicationPath = applicationPath;
        }

        public AssetBase GetAsset(string source)
        {
            if (source.StartsWith("~/") == false)
            {
                source = Path.Combine(context.DefaultPath, source);    
            }

            return new FileAsset(new AssetFile(source, server));
        }        

        public ICollection<AssetBase> GetAssets(FromDirectoryComponent component)
        {
            var collecton = new List<AssetBase>();

            //get all files by the extension
            IEnumerable<string> fileNames = new List<string>(Directory.GetFiles(server.MapPath(component.Path)))
                .Where(name => name.EndsWith(component.Extension));

            //if the user has sepecidied additonal filtering, filter it
            if (IsFilteringRequired(component))
            {
                fileNames = FilterFiles(fileNames, component);
            }

            foreach (var fileName in fileNames)
            {
                //string  = fileName.Substring(applicationPath.Length);               
                var source = fileName.ReplaceFirst(applicationPath, "~/")
                            .Replace("\\", "/");
                collecton.Add(GetAsset(source));
            }

            return collecton;
        }

        private bool IsFilteringRequired(FromDirectoryComponent component)
        {
            return component.StartsWithCollection.Count > 0 ||
                component.EndsWithCollection.Count > 0 ||
                component.ContainsCollection.Count > 0;
        }

        private IEnumerable<string> FilterFiles(IEnumerable<string> fileNames, FromDirectoryComponent component)
        {
            var filteredFileNames = fileNames.Where(
                    (name) => CompareAgainstCollection(component.StartsWithCollection, (x) => Path.GetFileNameWithoutExtension(name).StartsWith(x)) ||
                    CompareAgainstCollection(component.EndsWithCollection, (x) => Path.GetFileNameWithoutExtension(name).EndsWith(x)) ||
                    CompareAgainstCollection(component.ContainsCollection, (x) => Path.GetFileNameWithoutExtension(name).Contains(x))
            );

            return filteredFileNames;
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
