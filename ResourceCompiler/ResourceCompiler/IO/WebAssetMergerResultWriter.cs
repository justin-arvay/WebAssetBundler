// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.IO;

    public class WebAssetMergerResultWriter : IWebAssetMergerResultWriter
    {
        private string extension;

        private IPathResolver resolver;
        private IDirectoryWriter dirWriter;

        public WebAssetMergerResultWriter(string extension, IPathResolver resolver, IDirectoryWriter dirWriter)
        {
            this.extension = extension;
            this.resolver = resolver;
            this.dirWriter = dirWriter;
        }


        public void Write(string path, WebAssetMergerResult result)
        {
            var filePath = resolver.Resolve(path, result.Version, result.Name, extension);
            var directoryName = Path.GetDirectoryName(filePath);

            //ensure we create the directory structure to the resource
            if (Directory.Exists(directoryName) == false)
            {
                dirWriter.Write(directoryName);
            }

            //if dir doesnt exist, write it
            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(result.Content);
            }
        }
    }
}
