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
using System;

    [TestFixture]
    public class BundlesCacheTests
    {
        private Mock<ICacheProvider> provider;
        private BundlesCache<BundleImpl> cache;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ICacheProvider>();
            cache = new BundlesCache<BundleImpl>(provider.Object);
        }

        [Test]
        public void Should_Get_Bundle()
        {
            var bundle = new BundleImpl();

            provider.Setup(p => p.Get("test-BundleImpl-Bundle")).Returns((object)bundle);

            Assert.AreEqual(bundle, cache.Get("test"));

        }

        [Test]
        public void Should_Get_Null()
        {
            provider.Setup(p => p.Get("test-BundleImpl-Bundle")).Returns(null);

            Assert.IsNull(cache.Get("test"));
        }

        [Test]
        public void Should_Add_Bundle()
        {
            var bundle = new BundleImpl();
            bundle.Name = "test";

            cache.Add(bundle);

            provider.Verify(p => p.Insert("test-BundleImpl-Bundle", bundle), Times.Once());
        }
    }
}
