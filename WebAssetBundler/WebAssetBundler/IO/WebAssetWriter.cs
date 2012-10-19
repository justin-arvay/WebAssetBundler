// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Web;

    public class WebAssetWriter : IWebAssetWriter
    {
        private IDirectoryWriter dirWriter;
        private HttpServerUtilityBase server;

        public WebAssetWriter(IDirectoryWriter dirWriter, HttpServerUtilityBase server)
        {
            this.dirWriter = dirWriter;
            this.server = server;
        }

        public void Write(MergerResult result)
        {
            var filePath = "";//server.MapPath(result.Path);            

            //ensure we create the directory structure to the asset
            //we need to be careful here as the version can look like a file
            if (Directory.Exists(Path.GetDirectoryName(filePath)) == false)
            {
                //pass the full path to the directory writer. Do not pass the directory only
                dirWriter.Write(filePath);
            }

            //if dir doesnt exist, write it
            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(result.Content);
            }
        }
    }
}
