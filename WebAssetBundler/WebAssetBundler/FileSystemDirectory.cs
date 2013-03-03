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

        public string Path
        {
            get 
            {
                return fullPath;
            }
        }

        public IFile GetFile(string filename)
        {
            throw new NotImplementedException();
        }

        public IDirectory GetDirectory(string path)
        {
            throw new NotImplementedException();
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
            return Parent == null ? this : ((FileSystemDirectory)Parent).GetRootDirectory();
        }
    }
}
