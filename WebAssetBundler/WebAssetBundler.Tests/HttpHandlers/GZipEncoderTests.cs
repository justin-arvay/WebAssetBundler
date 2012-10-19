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
    using NUnit.Framework;
    using Moq;
    using System.Web;
    using System.IO.Compression;
    using System.IO;

    [TestFixture]
    public class GZipEncoderTests
    {
        private Mock<HttpResponseBase> response;
        private GZipEncoder encoder;

        [SetUp]
        public void Setup()
        {
            encoder = new GZipEncoder();
            response = new Mock<HttpResponseBase>();
        }

        [Test]
        public void Should_Encode_Response()
        {
            var stream = new MemoryStream();
            response.SetupGet(r => r.Filter).Returns(stream);
            encoder.Encode(response.Object);

            response.Verify(r => r.AppendHeader("Content-Encoding", "gzip"), Times.Once());
            response.Verify(r => r.AppendHeader("Vary", "Accept-Encoding"), Times.Once());
            response.VerifySet(r => r.Filter = It.IsAny<GZipStream>());
        }
    }
}
