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
    using TinyIoC;

    [TaskOrder(1)]
    public class ConfigureCommonTask : IBootstrapTask
    {
        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            container.Register<ICacheProvider, CacheProvider>();         
            container.Register<IAssetProvider, AssetProvider>();
            container.Register<IDirectorySearchFactory, DirectorySearchFactory>();
                      
            ConfigureHttpHandler(container);
        }

        public void ConfigureHttpHandler(TinyIoCContainer container)
        {
            container.Register<IHttpHandlerFactory, HttpHandlerFactory>()
                .AsSingleton();

            container.Register<IEncoderFactory, EncoderFactory>()
                .AsSingleton();

            container.Register<IResponseWriterFactory, ResponseWriterFactory>()
                .AsSingleton();
        }

        public void ShutDown()
        {

        }
    }
}
