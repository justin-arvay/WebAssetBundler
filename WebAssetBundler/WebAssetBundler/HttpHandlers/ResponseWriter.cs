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
    using System.IO;
    using System.IO.Compression;

    public class ResponseWriter : IResponseWriter
    {
        private HttpResponseBase response;
        private HttpRequestBase request;

        public ResponseWriter(HttpContextBase httpContext)
        {            
            this.response = httpContext.Response;
            this.request = httpContext.Request;
        }

        public void WriteAsset(MergerResult result, IEncoder encoder)
        {
            response.ContentType = result.ContentType;
            CacheLongTime(result.Hash.ToHexString());

            response.Write(result.Content);

            encoder.Encode(response);                                             
        }

        public bool IsNotModified(MergerResult result)
        {
            var actualETag = result.Hash.ToHexString();
            var givenETag = request.Headers["If-None-Match"];
            return givenETag == actualETag;
        }

        public void WriteNotFound()
        {
            response.StatusCode = 404;
        }

        public void WriteNotModified(string etag)
        {
            CacheLongTime(etag); // Some browsers seem to require a reminder to keep caching?!
            response.StatusCode = 304; // Not Modified
            response.SuppressContent = true;
        }

        private void CacheLongTime(string actualETag)
        {
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(DateTime.UtcNow.AddYears(1));
            response.Cache.SetETag(actualETag);
        }

    }
}
