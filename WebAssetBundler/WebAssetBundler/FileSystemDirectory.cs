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

    public class FileSystemDirectory : IDirectory
    {
        private string fullPath;

        public FileSystemDirectory(string fullPath)
        {
            this.fullPath = fullPath;
        }

        public System.IO.FileAttributes Attributes
        {
            get 
            {
                return File.GetAttributes(fullPath);
            }
        }

        public string FullPath
        {
            get 
            {
                return fullPath;
            }
        }

        public IFile GetFile(string filename)
        {
            if (filename.Replace('\\', '/').StartsWith(fullPath))
            {
                filename = filename.Substring(fullPath.Length + 1);
            }

            var directory = GetDirectory(Path.GetDirectoryName(filename));
            var filePath = Path.Combine(directory.FullPath, Path.GetFileName(filename));

            return new FileSystemFile(filePath, directory);
        }

        public IDirectory GetDirectory(string path)
        {
            if (path.Length == 0)
            {
                return this;
            }

            if (path[0] == '~')
            {
                path = path.Length == 1 ? "" : path.Substring(2);

                return GetRootDirectory().GetDirectory(path);
            }

            return new FileSystemDirectory(GetAbsolutePath(path))
            {
                Parent = this
            };
        }

        public IEnumerable<IDirectory> GetDirectories()
        {
            return Directory.GetDirectories(fullPath)
                .Select((d) =>
                {
                    return new FileSystemDirectory(d)
                    {
                        Parent = this
                    };
                });
        }

        public IEnumerable<IFile> GetFiles(string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(fullPath, searchPattern, searchOption)
                .Select((f) => new FileSystemFile(f, this));
        }

        public IDirectory Parent
        {
            get;
            private set;
        }

        public FileSystemDirectory GetRootDirectory()
        {
            //If it is already the root, return the same directory object, otherwise get parent
            return Parent == null ? this : ((FileSystemDirectory)Parent).GetRootDirectory();
        }

        public string GetAbsolutePath(string filename)
        {
            if (filename.StartsWith("~/"))
            {
                return GetRootDirectory().GetAbsolutePath(filename.Substring(2));
            }

            return Path.Combine(fullPath, filename);
        }

        public bool Exists
        {
            get 
            { 
                return Directory.Exists(fullPath); 
            }
        }

        public bool IsRoot
        {
            get { throw new NotImplementedException(); }
        }
    }
}
