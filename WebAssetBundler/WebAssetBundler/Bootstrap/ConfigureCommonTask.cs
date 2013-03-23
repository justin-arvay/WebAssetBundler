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
    using System.Web;

        [TaskOrder(1)]
    public class ConfigureCommonTask : IBootstrapTask
    {
        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            container.Register((c, p) => HttpContext());
            container.Register<HttpServerUtilityBase>(HttpContext().Server);
            container.Register((c, p) => HttpContext().Request);
            container.Register((c, p) => HttpContext().Response);
            container.Register((c, p) => HttpContext().Server);
            container.Register<ICacheProvider, CacheProvider>();
            container.Register<IDirectoryFactory, DirectoryFactory>();
            container.Register<ITypeProvider>(typeProvider);

            ConfigureHttpHandler(container);
        }

        public void ConfigureHttpHandler(TinyIoCContainer container)
        {
            container.Register<HttpHandlerFactory>()
                .AsSingleton();
            container.Register<EncoderFactory>()
                .AsSingleton();
        }

        private HttpContextBase HttpContext()
        {
            return new HttpContextWrapper(System.Web.HttpContext.Current);
        }
    }
}
