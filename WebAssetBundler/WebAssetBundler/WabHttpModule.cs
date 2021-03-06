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
    using System.Web;

    public class WabHttpModule : IHttpModule
    {
        private static readonly object locker = new object();
        private static WebHost host;

        public static WebHost Host
        {
            get { return host; }
        }

        public void Init(HttpApplication context)
        {
            lock (locker)
            {
                if (host == null)
                {
                    host = new WebHost();
                    host.Initialize();
                    host.RunBootstrapTasks();
                }
            }
        }

        public void Dispose()
        {
            lock (locker)
            {
                if (host != null)
                {
                    host.Dispose();
                }
            }
        }
    }
}
