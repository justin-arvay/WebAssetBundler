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
    using System.Collections.Generic;

    [TestFixture]
    public class BundleMetadataProviderTests
    {
        private BundleMetadataProvider provider;
        private Mock<IBundleMetadataCachePrimer> primer;
        private Mock<IBundleMetadataCache> cache;

        [SetUp]
        public void Setup()
        {
            primer = new Mock<IBundleMetadataCachePrimer>();
            cache = new Mock<IBundleMetadataCache>();
            provider = new BundleMetadataProvider(cache.Object, primer.Object);
        }

        [Test]
        public void Should_Prime_Cache()
        {
            provider.GetMetadata<BundleImpl>("Test");

            primer.Verify(p => p.Prime());
            primer.Verify(p => p.IsPrimed == true);
        }

        [Test]
        public void Should_Load_Metadata()
        {
            var cacheMetadata = new BundleMetadata();

            cache.Setup(c => c.Get<BundleImpl>("Test"))
                .Returns(cacheMetadata);

            BundleMetadata metadata = provider.GetMetadata<BundleImpl>("Test");

            Assert.AreSame(cacheMetadata, metadata);
            cache.Verify(c => c.Get<BundleImpl>("Test"));
        }

        [Test]
        public void Should_Return_Null_If_No_Metadata()
        {
            BundleMetadata metadata = provider.GetMetadata<BundleImpl>("Test");

            Assert.Null(metadata);
            cache.Verify(c => c.Get<BundleImpl>("Test"));
        }


        [Test]
        public void Should_Always_Prime_In_Debug_Mode()
        {
            Assert.Fail();
        }
    }
}
