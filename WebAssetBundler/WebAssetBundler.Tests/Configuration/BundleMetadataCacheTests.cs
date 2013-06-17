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
    public class BundleMetadataCacheTests
    {
        private Mock<ICacheProvider> provider;
        private BundleMetadataCache cache;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ICacheProvider>();
            cache = new BundleMetadataCache(provider.Object);
        }

        [Test]
        public void Should_Get_Metadata()
        {
            provider.Setup(p => p.Get("Test-BundleImpl"))
                .Returns(new BundleMetadata());

            BundleMetadata metadata = cache.Get<BundleImpl>("Test");

            Assert.IsInstanceOf<BundleMetadata>(metadata);
        }

        [Test]
        public void Should_Add_Metadata()
        {
            var metadata = new BundleMetadata()
            {
                Type = typeof(BundleImpl),
                Name = "Test"
            };

            string key = metadata.Name + "-" + metadata.Type.Name;

            cache.Add(metadata);

            provider.Verify(p => p.Insert(key, metadata));
        }
    }
}
