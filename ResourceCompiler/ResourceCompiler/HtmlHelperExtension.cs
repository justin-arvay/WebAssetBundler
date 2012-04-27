﻿// ResourceCompiler - Compiles web assets so you dont have to.
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



namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public static class HtmlHelperExtension
    {
        public static ComponentFactory Reco(this HtmlHelper helper)
        {
            var cacheProvider = new CacheProvider();
            return new ComponentFactory(helper.ViewContext, cacheProvider);   
        }

        /*
        public class SingletonPerRequest
        {
            public static SingletonPerRequest Current
            {
                get
                {
                    return (HttpContext.Current.Items["SingletonPerRequest"] ??
                        (HttpContext.Current.Items["SingletonPerRequest"] =
                        new SingletonPerRequest())) as SingletonPerRequest;

                }
            }
        }*/
    }
}
