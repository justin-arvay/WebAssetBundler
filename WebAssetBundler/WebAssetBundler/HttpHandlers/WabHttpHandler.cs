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
        private HttpHandlerFactory handlerFactory;
        private EncoderFactory encoderFactory;


        public WabHttpHandler() : 
            this(
            WabHttpModule.Host.Container.Resolve<HttpHandlerFactory>(), 
            WabHttpModule.Host.Container.Resolve<EncoderFactory>())
        {

        }

        public WabHttpHandler(HttpHandlerFactory handlerFactory, EncoderFactory encoderFactory)
        {            
            this.handlerFactory = handlerFactory;
            this.encoderFactory = encoderFactory;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {            
            var contextWrapper = new HttpContextWrapper(context);
            var writer = new ResponseWriter(contextWrapper);

            var handler = handlerFactory.Create(contextWrapper);

            if (handler is AssetHttpHandler<ImageBundle>)
            {
                writer = new ImageResponseWriter(contextWrapper);
            }

            handler.ProcessRequest(context.Request.PathInfo, writer, encoderFactory.Create(contextWrapper.Request));
        }
    }
}
