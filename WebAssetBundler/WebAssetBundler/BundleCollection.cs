﻿// WebAssetBundler - Bundles web assets so you dont have to.
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

    public class BundleCollection<TBundle> : List<TBundle> 
        where TBundle : Bundle
    {
        public BundleCollection()
        {
        }

        public BundleCollection(IList<TBundle> bundles)
        {
            this.AddRange(bundles);
        }

        /// <summary>
        /// Finds a assets bundle by name. If none is found returns null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TBundle FindBundleByName(string name) 
        {
            return this.SingleOrDefault(g => g.Name.IsCaseInsensitiveEqual(name));
        }
    }
}
