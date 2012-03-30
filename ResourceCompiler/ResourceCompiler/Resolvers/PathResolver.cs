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
    using System.Web;
    using System.Web.Hosting;

    public class PathResolver : IPathResolver
    {
        public PathResolver()
        {
        }

        public string Resolve(string path, string version, string fileName, string fileExt)
        {
            fileName = fileName + "." + fileExt;
            path = Path.Combine(path, version);
            path = HostingEnvironment.MapPath(path);

            //move this out of here, the path resolver should not need to generate a directory
            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileName);

            return path;
        }
    }
}
