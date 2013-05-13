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

    public abstract class Component
    {
        private IDictionary<string, string> attributes = new Dictionary<string, string>();

        public IDictionary<string, string> Attributes
        {
            get
            {
                return attributes;
            }
            private set;
        }

        public string Id
        {
            get
            {
                if (attributes.ContainsKey("id") == false)
                {
                    return string.Empty;
                }

                return attributes["id"];
            }
            set
            {
                attributes.Add("id", value);
            }
        }

        public string Name
        {
            get
            {
                if (attributes.ContainsKey("name") == false)
                {
                    return string.Empty;
                }

                return attributes["name"];
            }
            set
            {
                attributes.Add("name", value);
            }
        }

        public void AddCssClass(string cssClass)
        {
            string key = "class";
            if (attributes.ContainsKey(key))
            {
                var newValue = attributes[key] + " " + cssClass;
                attributes.Add(key, newValue);
            }
            else
            {
                attributes.Add(key, cssClass);
            }
        }
    }
}
