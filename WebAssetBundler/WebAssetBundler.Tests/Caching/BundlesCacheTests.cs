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
    using System.Collections.Generic;

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
        public void Should_Get_Bundles()
        {
            var bundles = new List<BundleImpl>();
            bundles.Add(new BundleImpl());

            provider.Setup(p => p.Get("BundleImpl-Bundles")).Returns((object)bundles);

            Assert.AreEqual(1, cache.Get().Count);

        }

        [Test]
        public void Should_Get_Null()
        {
            provider.Setup(p => p.Get("BundleImpl-Bundles")).Returns(null);

            Assert.IsNull(cache.Get());
        }

        [Test]
        public void Should_Set_Bundles()
        {
            var bundles = new BundleCollection<BundleImpl>();
            bundles.Add(new BundleImpl());

            cache.Set(bundles);

            provider.Verify(p => p.Insert("BundleImpl-Bundles", bundles));
        }

        [Test]
        public void Should_Not_Insert()
        {
            var bundles = new BundleCollection<BundleImpl>();
            bundles.Add(new BundleImpl());

            provider.Setup(p => p.Get("BundleImpl-Bundles")).Returns(new List<BundleImpl>());

            cache.Set(bundles);

            provider.Verify(p => p.Insert("BundleImpl-Bundles", bundles), Times.Never());
        }

        [Test]
        public void Should_Add_Bundle()
        {
            var bundle = new BundleImpl();
            bundle.Name = "test";
            var bundles = new List<BundleImpl>();

            provider.Setup(p => p.Get("BundleImpl-Bundles")).Returns(bundles);

            cache.Add(bundle);

            provider.Verify(p => p.Insert("BundleImpl-Bundles", bundles), Times.Once());
        }
    }
}
