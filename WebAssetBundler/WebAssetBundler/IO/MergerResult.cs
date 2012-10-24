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
    using System.Security.Cryptography;

    public class MergerResult
    {
        private WebAssetType type;

        public MergerResult(string name, string content, WebAssetType type)
        {            
            Name = name;
            Content = content;
            this.type = type;
        }

        public string Name
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

        public string ContentType
        {
            get
            {
                switch (type)
                {
                    case WebAssetType.Script:
                        return "text/javascript";
                    case WebAssetType.StyleSheet:
                        return "text/css";
                    default:
                        return "text/unknown";
                }
            }
        }

        public byte[] Hash
        {
            get
            {
                MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
                byte[] bs = Encoding.UTF8.GetBytes(Content);
                return  x.ComputeHash(bs);
            }
        }
    }
}
