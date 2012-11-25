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
    public class BundleConfigurationCollectionTests
    {
        private BundleConfigurationCollection<BundleConfigurationImpl, BundleImpl> collection;
        private IList<BundleConfigurationImpl> list;
        private Mock<IAssetLocator<FromDirectoryComponent>> locator;


        [SetUp]
        public void Setup()
        {
            list = new List<BundleConfigurationImpl>();
            locator = new Mock<IAssetLocator<FromDirectoryComponent>>();
            collection = new BundleConfigurationCollection<BundleConfigurationImpl, BundleImpl>(list, locator.Object);
        }

        [Test]
        public void Should_Get_Bundles()
        {
            var config = new Mock<BundleConfigurationImpl>();

            list.Add(config.Object);

            var bundles = collection.GetBundles();

            Assert.AreEqual(1, bundles.Count);
            Assert.AreEqual(locator.Object, config.Object.AssetLocator);
            config.Verify(c => c.Configure(), Times.Once());
        }
    }
}
