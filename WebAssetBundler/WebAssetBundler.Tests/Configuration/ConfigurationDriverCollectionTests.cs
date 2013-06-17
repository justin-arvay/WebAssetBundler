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
    public class ConfigurationDriverCollectionTests
    {
        private ConfigurationDriverCollection collection;

        [SetUp]
        public void Setup()
        {
            collection = new ConfigurationDriverCollection();
        }

        [Test]
        public void Should_Load_Driver()
        {
            var driver = new Mock<IConfigurationDriver>();
            driver.Setup(d => d.LoadMetadata())
                .Returns(new List<BundleMetadata>() {
                    new BundleMetadata()
                });

            collection.Add(driver.Object);

            var metadatas = (IList<BundleMetadata>)collection.LoadMetadata();

            Assert.IsInstanceOf<IEnumerable<BundleMetadata>>(metadatas);
            Assert.AreEqual(1, metadatas.Count());
        }
    }
}
