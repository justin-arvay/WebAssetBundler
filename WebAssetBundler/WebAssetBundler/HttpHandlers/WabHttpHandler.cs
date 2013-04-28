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
    using System.Web;

    public class WabHttpHandler : IHttpHandler
    {
        private IHttpHandlerFactory handlerFactory;
        private IEncoderFactory encoderFactory;
        private IResponseWriterFactory writerFactory;


        public WabHttpHandler() : 
            this(
            WabHttpModule.Host.Container.Resolve<IHttpHandlerFactory>(),
            WabHttpModule.Host.Container.Resolve<IEncoderFactory>(),
            WabHttpModule.Host.Container.Resolve<IResponseWriterFactory>())
        {

        }

        public WabHttpHandler(IHttpHandlerFactory handlerFactory, IEncoderFactory encoderFactory, IResponseWriterFactory writerFactory)
        {            
            this.handlerFactory = handlerFactory;
            this.encoderFactory = encoderFactory;
            this.writerFactory = writerFactory;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {            
            var contextWrapper = new HttpContextWrapper(context);
            var writer = writerFactory.Create(contextWrapper);
            var handler = handlerFactory.Create(contextWrapper);

            handler.ProcessRequest(context.Request.PathInfo, writer, encoderFactory.Create(contextWrapper.Request));
        }
    }
}
