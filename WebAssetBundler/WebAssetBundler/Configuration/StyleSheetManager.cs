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
    using WebAssetBundler.Web.Mvc;

    public class StyleSheetManager
    {
        public StyleSheetManager(BundleCollection bundles)
        {
            StyleSheetBundles = bundles;
        }

        public BundleCollection StyleSheetBundles
        {
            get;
            private set;
        }
    }
}