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
    public class StyleSheetBundleProviderTests
    {
        private StyleSheetBundleProvider provider;
        private Mock<IStyleSheetConfigProvider> configProvider;
        private Mock<IBundlesCache<StyleSheetBundle>> cache;
        private Mock<IAssetLocator<FromDirectoryComponent>> locator;

        [SetUp]
        public void Setup()
        {
            configProvider = new Mock<IStyleSheetConfigProvider>();
            cache = new Mock<IBundlesCache<StyleSheetBundle>>();
            locator = new Mock<IAssetLocator<FromDirectoryComponent>>();
            provider = new StyleSheetBundleProvider(configProvider.Object, cache.Object, locator.Object);
        }

        [Test]
        public void Should_Get_Bundles()
        {
            var configs = new List<StyleSheetBundleConfiguration>();
            configs.Add(new StyleSheetBundleConfigurationImpl());

            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            var bundles = provider.GetBundles();
            Assert.AreEqual(1, bundles.Count);
            cache.Verify(c => c.Get(), Times.Once());
            cache.Verify(c => c.Set(bundles), Times.Once());
        }

        [Test]
        public void Should_Get_Bundles_From_Cache()
        {
            var collection = new BundleCollection<StyleSheetBundle>();
            collection.Add(new StyleSheetBundle());

            cache.Setup(c => c.Get()).Returns(collection);

            var bundles = provider.GetBundles();

            Assert.AreEqual(1, bundles.Count);
            cache.Verify(c => c.Set(collection), Times.Never());

        }
    }
}
