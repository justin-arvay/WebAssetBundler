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

    public interface IDirectory
    {
        FileAttributes Attributes { get; }
        string FullPath { get; }

        /// <summary>
        /// Allows you to check if this directory is considered the root directory. A root directory can
        /// be the first directory created in a heirarchy. It does not mean this is the root directory of the application.
        /// Think of it as the root directory under the context in which it is being used.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Returns the parent directory.
        /// </summary>
        IDirectory Parent { get; }

        /// <summary>
        /// Checks if the directory exists. Only way to ensure a directory is real.
        /// </summary>
        bool Exists { get; }
        
        /// <summary>
        /// Gets a file in the current directory, or a file based on a virtual path. The root of the directory tree is defined as
        /// the first directory created (aka the highest level parent).
        /// 
        /// Eg: 
        /// d1 = Directory Root: C:\webroot\app\
        /// d2 = Directory Under Root: C:\webroot\app\Content\
        /// 
        /// d2.GetFile("~/Content/test.css") would return c:\webroot\app\Content\test.css
        /// The above returned an IFile based on the directory root.
        /// 
        /// d2.GetFile("test.css") would return c:\webroot\app\test.css
        /// The above returned an IFile based on the current directory object        
        /// 
        /// Note: The IFile returned does not garentee that the file actually exists. You need to use the Exists function.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        IFile GetFile(string filename);

        /// <summary>
        /// Gets a dirrectory in the current directory or a directory defined by a virtual path. The root of the directory tree is defined as
        /// the first directory created (aka the highest level parent).
        /// 
        /// d1 = Directory Root: C:\webroot\app\
        /// d2 = Directory Under Root: C:\webroot\app\Content\
        /// 
        /// d2.GetDirectory("~/Content/Test") would return c:\webroot\app\Content\Test
        /// The above returned an IDirectory based on the directory root.
        /// 
        /// d2.GetDirectory("Test") would return c:\webroot\app\Test\
        /// The above returned an IDirectory based on the current directory object
        /// 
        /// d2.GetDirectory("Test/File.css") would return c:\webroot\app\Test\
        /// The above returned an IDirectory based on the current directory object and ignoring the file
        /// 
        /// Note: The IDirectory returned does not garentee that the file actually exists. You need to use the Exists function.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IDirectory GetDirectory(string path);

        /// <summary>
        /// Gets all directories in current directory.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDirectory> GetDirectories();

        /// <summary>
        /// Gets all files in current directory based on pattern and search options. Pattern can use wildcards. Example:
        /// Eg: *.ext would return all files ending with the extension "ext".
        /// 
        /// Note: Also supports mutliple wildcards.
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        IEnumerable<IFile> GetFiles(string searchPattern, SearchOption searchOption);
    }
}
