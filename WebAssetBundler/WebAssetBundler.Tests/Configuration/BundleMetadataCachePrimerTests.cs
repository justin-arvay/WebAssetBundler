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
    using System;
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class BundleMetadataCachePrimerTests
    {
        private BundleMetadataCachePrimer primer;
        private Mock<IConfigurationDriver> driver;
        private Mock<IBundleMetadataCache> cache;

        [SetUp]
        public void Setup()
        {
            driver = new Mock<IConfigurationDriver>();
            cache = new Mock<IBundleMetadataCache>();
            primer = new BundleMetadataCachePrimer(driver.Object, cache.Object);
        }

        [Test]
        public void Should_Prime_Cache()
        {
            var metadata = new BundleMetadata();
            var metadatas = new List<BundleMetadata>()
            {
                metadata
            };

            driver.Setup(d => d.LoadMetadata())
                .Returns(metadatas);

            primer.Prime();

            cache.Verify(c => c.Add(metadata));
            Assert.True(primer.IsPrimed);
        }
    }
}
