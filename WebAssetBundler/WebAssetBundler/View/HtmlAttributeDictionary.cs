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

    public class HtmlAttributeDictionary : Dictionary<string, string>
    {
        public string Id
        {
            get
            {
                if (ContainsKey("id") == false)
                {
                    return string.Empty;
                }

                return this["id"];
            }
            set
            {
                Add("id", value);
            }
        }

        public string Name
        {
            get
            {
                if (ContainsKey("name") == false)
                {
                    return string.Empty;
                }

                return this["name"];
            }
            set
            {
                Add("name", value);
            }
        }

        public void AddClass(string cssClass)
        {
            string key = "class";
            if (ContainsKey(key))
            {
                var newValue = this[key] + " " + cssClass;
                this[key] = newValue;
            }
            else
            {
                Add(key, cssClass);
            }
        }
    }
}
