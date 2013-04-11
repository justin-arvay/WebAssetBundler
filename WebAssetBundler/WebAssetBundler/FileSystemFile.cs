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
        private string path;

        public FileSystemFile(string path)
        {
            this.path = path;
        }

        public FileSystemFile(string path, IDirectory directory)
        {
            this.path = path;
            Directory = directory;
        }

        public bool Exists
        {
            get 
            {
                return File.Exists(path);
            }
        }

        public string Path
        {
            get 
            {
                return path;
            }
        }


        public Stream Open(FileMode mode)
        {
            return File.Open(path, mode);
        }

        public Stream Open(FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access);
        }

        public Stream Open(FileMode mode, FileAccess access, FileShare fileShare)
        {
            return File.Open(path, mode, access, fileShare);
        }

        public IDirectory Directory
        {
            get;
            private set;
        }
    }
}
