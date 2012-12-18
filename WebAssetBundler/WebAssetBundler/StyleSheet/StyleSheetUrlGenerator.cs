﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

    public class StyleSheetUrlGenerator : IUrlGenerator<StyleSheetBundle>
    {

        public string Generate(string name, string version, string host, BundleContext context)
        {
            if (context.DebugMode)
            {
                version = context.CreateCacheBreakerVersion(version);
            }

            var path = "wab.axd/css/{0}/{1}";

            host = host ?? "";

            if (host.EndsWith("/") == false)
            {
                host += "/";
            }

            return host + path.FormatWith(version, name);
        }
    }
}
