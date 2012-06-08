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

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;

    public class WebAssetGroupCollection : Collection<WebAssetGroup>
    {

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public WebAssetGroupCollection()
        {
        }

        /// <summary>
        /// Finds a resource group by name. If none is found returns null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public WebAssetGroup FindGroupByName(string name)
        {
            return Items.SingleOrDefault(g => g.Name.IsCaseInsensitiveEqual(name));
        }

    }
}
