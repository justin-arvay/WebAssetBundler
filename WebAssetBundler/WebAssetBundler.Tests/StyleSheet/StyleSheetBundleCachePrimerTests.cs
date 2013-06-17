﻿
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
    public class StyleSheetBundleCachePrimerTests
    {
        private BundleMetadataCachePrimer primer;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<StyleSheetBundle>> pipeline;
        private Mock<BundleCache<StyleSheetBundle>> cache;
        private Mock<IDirectorySearchFactory> dirSearchProvider;

        [SetUp]
        public void Setup()
        {
            assetProvider = new Mock<IAssetProvider>();
            pipeline = new Mock<IBundlePipeline<StyleSheetBundle>>();
            cache = new Mock<BundleCache<StyleSheetBundle>>();
            dirSearchProvider = new Mock<IDirectorySearchFactory>();
            primer = new BundleMetadataCachePrimer(assetProvider.Object, pipeline.Object, cache.Object, dirSearchProvider.Object);
        }

        [Test]
        public void Should_Be_Primed()
        {
            primer.Prime(new List<IFluentConfiguration<StyleSheetBundle>>());

            Assert.IsTrue(primer.IsPrimed);
        }

        [Test]
        public void Should_Prime_Cache()
        {
            var configOne = new StyleSheetBundleConfigurationImpl();
            var configTwo = new StyleSheetBundleConfigurationImpl();

            var configs = new List<IFluentConfiguration<StyleSheetBundle>>();
            configs.Add(configOne);
            configs.Add(configTwo);

            primer.Prime(configs);

            cache.Verify(c => c.Add(It.IsAny<StyleSheetBundle>()), Times.Exactly(2));
            pipeline.Verify(p => p.Process(It.IsAny<StyleSheetBundle>()), Times.Exactly(2));
            Assert.AreEqual(1, configOne.CallCount);
            Assert.AreEqual(1, configTwo.CallCount);
            Assert.IsInstanceOf<IAssetProvider>(configOne.AssetProvider);
            Assert.IsInstanceOf<IAssetProvider>(configTwo.AssetProvider);
            Assert.IsInstanceOf<IDirectorySearchFactory>(configOne.DirectorySearchFactory);
            Assert.IsInstanceOf<IDirectorySearchFactory>(configTwo.DirectorySearchFactory);
            Assert.IsInstanceOf<StyleSheetBundle>(configOne.Bundle);
            Assert.IsInstanceOf<StyleSheetBundle>(configTwo.Bundle);
            Assert.AreEqual("StyleSheetBundleConfigurationImpl", configOne.Bundle.Name);
            Assert.AreEqual("StyleSheetBundleConfigurationImpl", configTwo.Bundle.Name);

        }

        [Test]
        public void Should_Not_Prime_Cache()
        {
            primer.Prime(new List<IFluentConfiguration<StyleSheetBundle>>());

            cache.Verify(c => c.Add(It.IsAny<StyleSheetBundle>()), Times.Never());
        }
    }
}
