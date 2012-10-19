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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Web;
    using System.IO;
    using System.Collections.Specialized;
    using System;

    [TestFixture]
    public class ResponseWriterTests
    {
        private Mock<HttpContextBase> httpContext;
        private IResponseWriter writer;
        private Mock<HttpRequestBase> request;
        private Mock<HttpResponseBase> response;
        private Mock<HttpCachePolicyBase> cache;
        private Mock<IEncoder> encoder;

        [SetUp]
        public void Setup()
        {
            request = new Mock<HttpRequestBase>();
            response = new Mock<HttpResponseBase>();
            cache = new Mock<HttpCachePolicyBase>();
            encoder = new Mock<IEncoder>();

            response.Setup(r => r.Cache).Returns(cache.Object);
            response.Setup(r => r.Headers).Returns(new NameValueCollection());

            httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(request.Object);
            httpContext.Setup(c => c.Response).Returns(response.Object);
            
            writer = new ResponseWriter(httpContext.Object);
        }

        [Test]
        public void Should_Set_Headers_For_Asset()
        {
            var result = new MergerResult("name", "1.1", "content", WebAssetType.None);
            var collection = new NameValueCollection();
            collection.Add("Accept-Encoding", "some encoding");

            request.Setup(r => r.Headers).Returns(collection);
            writer.WriteAsset(result, encoder.Object);

            cache.Verify(c => c.SetETag("-2012792703"));
            cache.Verify(c => c.SetExpires(It.Is<DateTime>(e => e.ToShortDateString() == DateTime.UtcNow.AddYears(1).ToShortDateString())));
            cache.Verify(c => c.SetCacheability(HttpCacheability.Public));
            response.Verify(r => r.Write("content"));
            encoder.Verify(e => e.Encode(response.Object), Times.Once());
        }

        [Test]
        public void Should_Set_Not_Found_Headers()
        {
            writer.WriteNotFound();

            response.VerifySet(r => r.StatusCode = 404);
        }

        [Test]
        public void Should_Set_Not_Modified_Headers()
        {
            writer.WriteNotModified("eeeeee");

            response.VerifySet(r => r.StatusCode = 304);
            response.VerifySet(r => r.SuppressContent = true);
            cache.Verify(c => c.SetETag("eeeeee"));
            cache.Verify(c => c.SetExpires(It.Is<DateTime>(e => e.ToShortDateString() == DateTime.UtcNow.AddYears(1).ToShortDateString())));
            cache.Verify(c => c.SetCacheability(HttpCacheability.Public));
        }

        [Test]
        public void Should_Be_Modified()
        {
            var result = new MergerResult("name", "1.1", "", WebAssetType.None);
            var collection = new NameValueCollection();
            collection.Add("If-None-Match", "");

            request.Setup(r => r.Headers).Returns(collection);

            Assert.IsFalse(writer.IsNotModified(result));
        }

        [Test]
        public void Should_Not_Be_Modified()
        {
            var result = new MergerResult("name", "1.1", "", WebAssetType.None);
            var collection = new NameValueCollection();
            collection.Add("If-None-Match", result.GetHashCode().ToString());

            request.Setup(r => r.Headers).Returns(collection);

            Assert.IsTrue(writer.IsNotModified(result));
        }
    }
}
