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

    public class HttpHandlerFactory : IHttpHandlerFactory
    {
        private ICacheProvider cacheProvider;

        public HttpHandlerFactory(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }

        public IWabHttpHandler Create(HttpContextBase httpContext)
        {
            var urlExtension = httpContext.Request.PathInfo;

            if (urlExtension.StartsWith("/js"))
            {
                var cache = new BundleCache<ScriptBundle>(cacheProvider); 
                return new AssetHttpHandler<ScriptBundle>(cache);
            }

            if (urlExtension.StartsWith("/css"))
            {
                var cache = new BundleCache<StyleSheetBundle>(cacheProvider);
                return new AssetHttpHandler<StyleSheetBundle>(cache);
            }

            if (urlExtension.StartsWith("/image"))
            {
                var cache = new BundleCache<ImageBundle>(cacheProvider);
                return new AssetHttpHandler<ImageBundle>(cache);
            }

            throw new HttpException(404, "Resource not found.");
        }
    }
}
