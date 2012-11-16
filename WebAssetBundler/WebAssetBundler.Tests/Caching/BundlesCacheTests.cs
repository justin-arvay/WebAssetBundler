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
            var bundles = new List<Bundle>();
            bundles.Add(new BundleImpl());

            provider.Setup(p => p.Get("Bundles")).Returns(bundles);

            Assert.AreEqual(1, cache.Get().Count);

        }

        [Test]
        public void SHould_Set_Bundles()
        {
            var bundles = new List<BundleImpl>();
            bundles.Add(new BundleImpl());

            cache.Set(bundles);

            provider.Verify(p => p.Insert("Bundles", bundles));
        }
    }
}
