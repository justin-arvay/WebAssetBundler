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
        private Mock<IBundlesCache<BundleImpl>> cache;
        private Mock<IEncoder> encoder;
        private AssetHttpHandler<BundleImpl> handler;
        private BundleImpl bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
            writer = new Mock<IResponseWriter>();
            cache = new Mock<IBundlesCache<BundleImpl>>();
            encoder = new Mock<IEncoder>();
            handler = new AssetHttpHandler<BundleImpl>(cache.Object);

            cache.Setup(c => c.Get("name")).Returns(bundle);
        }

        [Test]
        public void Should_Write_Not_Modified()
        {
            bundle.Name = "name";
            writer.Setup(w => w.IsNotModified(bundle)).Returns(true);

            handler.ProcessRequest("/asset/1.1/name", writer.Object, encoder.Object);

            cache.Verify(c => c.Get("name"), Times.Once());
            writer.Verify(w => w.IsNotModified(bundle), Times.Once());
            writer.Verify(w => w.WriteNotModified(bundle.Hash.ToHexString()), Times.Once());
        }

        [Test]
        public void Should_Write_Asset()
        {
            bundle.Name = "name";
            writer.Setup(w => w.IsNotModified(bundle)).Returns(false);

            handler.ProcessRequest("/asset/1.1/name", writer.Object, encoder.Object);

            cache.Verify(c => c.Get("name"), Times.Once());
            writer.Verify(w => w.IsNotModified(bundle), Times.Once());
            writer.Verify(w => w.WriteAsset(bundle, encoder.Object), Times.Once());
        }

        [Test]
        public void Should_Write_Not_Found()
        {            
            handler.ProcessRequest("/asset/1.1/name", writer.Object, encoder.Object);

            cache.Verify(c => c.Get("name"), Times.Once());
            writer.Verify(w => w.WriteNotFound(), Times.Once());
        }

    }
}
