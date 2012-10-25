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

    [TestFixture]
    public class AssetHttpHandlerTests
    {
        private Mock<IResponseWriter> writer;
        private Mock<IMergedResultCache> cache;
        private Mock<IEncoder> encoder;
        private AssetHttpHandler handler;

        [SetUp]
        public void Setup()
        {
            writer = new Mock<IResponseWriter>();
            cache = new Mock<IMergedResultCache>();
            encoder = new Mock<IEncoder>();
            handler = new AssetHttpHandler(cache.Object);
        }

        [Test]
        public void Should_Write_Not_Modified()
        {
            var result = new MergerResult("name", "", WebAssetType.None);
            cache.Setup(c => c.Get(It.IsAny<string>())).Returns(result);
            writer.Setup(w => w.IsNotModified(result)).Returns(true);

            handler.ProcessRequest("/asset/1.1/name", writer.Object, encoder.Object);

            cache.Verify(c => c.Get(It.Is<string>(s => s.Equals("name"))), Times.Once());
            writer.Verify(w => w.WriteNotModified(result.Hash.ToHexString()), Times.Once());
        }

        [Test]
        public void Should_Write_Asset()
        {
            var result = new MergerResult("name", "", WebAssetType.None);
            cache.Setup(c => c.Get(It.IsAny<string>())).Returns(result);
            writer.Setup(w => w.IsNotModified(result)).Returns(false);

            handler.ProcessRequest("/asset/1.1/name", writer.Object, encoder.Object);

            cache.Verify(c => c.Get(It.Is<string>(s => s.Equals("name"))), Times.Once());
            writer.Verify(w => w.WriteAsset(result, encoder.Object), Times.Once());
        }

        [Test]
        public void Should_Write_Not_Found()
        {            
            handler.ProcessRequest("/asset/1.1/name", writer.Object, encoder.Object);

            cache.Verify(c => c.Get(It.Is<string>(s => s.Equals("name"))), Times.Once());
            writer.Verify(w => w.WriteNotFound(), Times.Once());
        }

    }
}
