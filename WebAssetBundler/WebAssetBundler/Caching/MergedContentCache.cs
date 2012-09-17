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

    public class MergedContentCache : IMergedContentCache
    {
        private ICacheProvider provider;
        private WebAssetType type;

        public MergedContentCache(WebAssetType type, ICacheProvider provider)
        {
            this.provider = provider;
            this.type = type;
        }

        public string Get(string name)
        {
            var content = (string)provider.Get(GetKey(name));

            return content ?? "";
        }

        public void Add(string name, string content)
        {
            provider.Insert(GetKey(name), content);
        }

        private string GetKey(string name)
        {
             var typePrefix = "";

            switch (type)
            {
                case WebAssetType.Script:
                    typePrefix = "Script";
                    break;
                case WebAssetType.StyleSheet:
                    typePrefix = "StyleSheet";
                    break;
            }

            return "MergedContent->" + typePrefix + "->" + name;
        }
    }
}
