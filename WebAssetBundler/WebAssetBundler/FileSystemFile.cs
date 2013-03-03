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
    using System.Web;
    using System.IO;

    public class FileSystemFile : IFile
    {
        private string fullPath;
        private string source;

        public FileSystemFile(string source)
        {
            fullPath = source;
            this.source = source;
        }

        public FileSystemFile(string source, HttpServerUtilityBase server)
        {
            fullPath = server.MapPath(source);
            this.source = source;
        }

        public bool Exists
        {
            get 
            {
                return File.Exists(fullPath);
            }
        }

        public string FullPath
        {
            get 
            {
                return fullPath;
            }
        }


        public Stream Open(FileMode mode)
        {
            return File.Open(fullPath, mode);
        }

        public Stream Open(FileMode mode, FileAccess access)
        {
            return File.Open(fullPath, mode, access);
        }

        public Stream Open(FileMode mode, FileAccess access, FileShare fileShare)
        {
            return File.Open(fullPath, mode, access, fileShare);
        }


        public string Source
        {
            get { return source; }
        }
    }
}
