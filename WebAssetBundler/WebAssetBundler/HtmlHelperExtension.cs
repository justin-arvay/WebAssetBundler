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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web;

    public static class HtmlHelperExtension
    {       

        public static ComponentBuilder Bundler(this HtmlHelper helper)
        {                                    
            var viewContext = helper.ViewContext;
            var cacheProvider = new CacheProvider();


            var builderFactory = new ManagerBuilderFactory(viewContext, cacheProvider, SharedGroups.Manager);

            var styleSheetManagerBuilder = (HttpContext.Current.Items["StyleSheetManagerBuilder"] ??
                        (HttpContext.Current.Items["StyleSheetManagerBuilder"] =
                        builderFactory.CreateStyleSheetManagerBuilder())) as StyleSheetManagerBuilder;

            var scriptManagerBuilder = (HttpContext.Current.Items["ScriptManagerBuilder"] ??
                        (HttpContext.Current.Items["ScriptManagerBuilder"] =
                        builderFactory.CreateScriptManagerBuilder())) as ScriptManagerBuilder;


            return new ComponentBuilder(
                styleSheetManagerBuilder,
                scriptManagerBuilder);
        }
     
    }
}
