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
    using System.IO;
    using System.IO.Compression;

    public class ResponseWriter : IResponseWriter
    {
        protected HttpResponseBase response;
        protected HttpRequestBase request;

        public ResponseWriter(HttpContextBase httpContext)
        {            
            this.response = httpContext.Response;
            this.request = httpContext.Request;
        }

        public virtual void WriteAsset(Bundle bundle, IEncoder encoder)
        {
            response.ContentType = bundle.ContentType;
            CacheLongTime(bundle.Hash.ToHexString(), bundle.BrowserTtl);

            bundle.Content.CopyTo(response.OutputStream);

            encoder.Encode(response);                                             
        }

        public virtual bool IsNotModified(Bundle bundle)
        {
            var actualETag = bundle.Hash.ToHexString();
            var givenETag = request.Headers["If-None-Match"];
            return givenETag == actualETag;
        }

        public virtual void WriteNotFound()
        {
            response.StatusCode = 404;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="etag"></param>
        /// <param name="ttl">In minutes.</param>
        public virtual void WriteNotModified(Bundle bundle)
        {
            CacheLongTime(bundle.Hash.ToHexString(), bundle.BrowserTtl); // Some browsers seem to require a reminder to keep caching?!
            response.StatusCode = 304; // Not Modified
            response.SuppressContent = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualETag"></param>
        /// <param name="ttl">In minutes.</param>
        protected void CacheLongTime(string actualETag, int ttl)
        {
            var expires = DateTime.UtcNow.AddMinutes(ttl);
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(expires);
            response.Cache.SetETag(actualETag);
        }

    }
}
