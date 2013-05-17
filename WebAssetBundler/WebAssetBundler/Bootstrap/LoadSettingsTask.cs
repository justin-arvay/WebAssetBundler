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
    using System.Web.Configuration;
    using System.Web;
    using TinyIoC;
    using System.IO;

    [TaskOrder(0)]
    public class LoadSettingsTask : IBootstrapTask
    {
        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {

            var section = (WebConfigurationManager.GetSection("wab") as WabConfigurationSection)
                   ?? new WabConfigurationSection();

           
            container.Register<SettingsContext>(CreateSettingsContext(section, container));
        }

        public SettingsContext CreateSettingsContext(WabConfigurationSection section, TinyIoCContainer container)
        {
            var httpContext = container.Resolve<HttpContextBase>();
            var rootPath = MappedApplicationPath(httpContext);

            return new SettingsContext
            {
                DebugMode = section.DebugMode,
                MinifyIdentifier = section.MinifyIdentifier,
                EnableImagePipeline = section.EnableImagePipeline,
                AppRootDirectory = new FileSystemDirectory(rootPath),
                HttpContext = httpContext
            };
        }

        public string MappedApplicationPath(HttpContextBase httpContext)
        {
            string appPath = httpContext.Request.ApplicationPath;

            return httpContext.Server.MapPath(appPath);
        }

        public void ShutDown()
        {

        }
    }
}
