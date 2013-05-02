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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;
    using Moq;
    using System.Web;
    using System.Collections.Specialized;
    using System.IO;

    [TestFixture]
    public class ImageResponseWriterTests
    {
        private Mock<HttpContextBase> httpContext;
        private ImageResponseWriter writer;
        private Mock<HttpRequestBase> request;
        private Mock<HttpResponseBase> response;
        private Mock<HttpCachePolicyBase> cache;
        private Mock<IEncoder> encoder;
        private Bundle bundle;

        [SetUp]
        public void Setup()
        {
            request = new Mock<HttpRequestBase>();
            response = new Mock<HttpResponseBase>();
            cache = new Mock<HttpCachePolicyBase>();
            encoder = new Mock<IEncoder>();
            bundle = new ImageBundle("image/jpeg", "/Image/test.jpg");

            response.Setup(r => r.Cache).Returns(cache.Object);
            response.Setup(r => r.Headers).Returns(new NameValueCollection());

            httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(request.Object);
            httpContext.Setup(c => c.Response).Returns(response.Object);

            writer = new ImageResponseWriter(httpContext.Object);
        }

        [Test]
        public void Should_Set_Headers_For_Asset()
        {
            bundle.BrowserTtl = 10;
            bundle.Assets.Add(new AssetBaseImpl("just a string converted to bytes for testing"));

            var collection = new NameValueCollection();
            collection.Add("Accept-Encoding", "some encoding");

            request.Setup(r => r.Headers).Returns(collection);
            response.Setup(x => x.OutputStream).Returns(new MemoryStream());

            writer.WriteAsset(bundle, encoder.Object);

            //reset position so i can test output
            response.Object.OutputStream.Position = 0;

            cache.Verify(c => c.SetExpires(It.Is<DateTime>((e) => e.Minute == DateTime.UtcNow.AddMinutes(bundle.BrowserTtl).Minute)));
            cache.Verify(c => c.SetETag(bundle.Hash.ToHexString()));
            cache.Verify(c => c.SetCacheability(HttpCacheability.Public));
            Assert.AreEqual(bundle.Content.ReadToEnd(), response.Object.OutputStream.ReadToEnd());
            encoder.Verify(e => e.Encode(response.Object), Times.Never());
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
            bundle.BrowserTtl = 10;
            writer.WriteNotModified(bundle);

            response.VerifySet(r => r.StatusCode = 304);
            response.VerifySet(r => r.SuppressContent = true);
            cache.Verify(c => c.SetETag("d41d8cd98f00b204e9800998ecf8427e"));
            cache.Verify(c => c.SetExpires(It.Is<DateTime>((e) => e.Minute == DateTime.UtcNow.AddMinutes(bundle.BrowserTtl).Minute)));
            cache.Verify(c => c.SetCacheability(HttpCacheability.Public));
        }

        [Test]
        public void Should_Be_Modified()
        {
            var collection = new NameValueCollection();
            collection.Add("If-None-Match", "");

            request.Setup(r => r.Headers).Returns(collection);

            Assert.IsFalse(writer.IsNotModified(bundle));
        }

        [Test]
        public void Should_Not_Be_Modified()
        {
            bundle.Assets.Add(new AssetBaseImpl("test"));

            var collection = new NameValueCollection();
            collection.Add("If-None-Match", bundle.Hash.ToHexString());

            request.Setup(r => r.Headers).Returns(collection);

            Assert.IsTrue(writer.IsNotModified(bundle));
        }
    }
}
