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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Policy;
    using System.IO;

    public class WebAssetMergerResult
    {
        public WebAssetMergerResult(string generatedPath, string content)
        {            
            Path = generatedPath;
            Content = content;
        }

        public string Path
        {
            get;
            private set;
        }

        public string Content
        {
            get;
            private set;
        }

        public string Host
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CreateUrl(string path, string host)
        {
            if (host != null && host.Length > 0)
            {
                if (path.StartsWith("/"))
                {
                    path = path.TrimStart('/');
                }

                return System.IO.Path.Combine(host, path).Replace("\\", "/");
            }

            return path;
        }
    }
}
