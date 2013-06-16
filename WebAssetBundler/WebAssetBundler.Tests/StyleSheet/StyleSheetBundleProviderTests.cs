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
    using System.Web;
    using System.IO;
using System;

    [TestFixture]
    public class StyleSheetBundleProviderTests
    {
        private StyleSheetBundleProvider provider;
        private Mock<IBundleConfigurationProvider<StyleSheetBundle>> configProvider;
        private Mock<IBundlesCache<StyleSheetBundle>> cache;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<StyleSheetBundle>> pipeline;
        private Mock<IBundleMetadataCachePrimer<StyleSheetBundle>> primer;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            pipeline = new Mock<IBundlePipeline<StyleSheetBundle>>();
            configProvider = new Mock<IBundleConfigurationProvider<StyleSheetBundle>>();
            cache = new Mock<IBundlesCache<StyleSheetBundle>>();
            assetProvider = new Mock<IAssetProvider>();
            primer = new Mock<IBundleMetadataCachePrimer<StyleSheetBundle>>();

            provider = new StyleSheetBundleProvider(configProvider.Object, cache.Object, pipeline.Object, 
                assetProvider.Object, primer.Object, settings);
        }

        [Test]
        public void Should_Get_Named_Bundle()
        {
            var bundle = new StyleSheetBundle();
            cache.Setup(c => c.Get("test")).Returns(bundle);

            var bundleOut = provider.GetNamedBundle("test");

            cache.Verify(c => c.Get("test"), Times.Once());
            Assert.AreSame(bundleOut, bundle);
        }

        [Test]
        public void Should_Prime_Cache_When_Getting_Named_Bundle()
        {
            var configs = new List<IFluentConfiguration<StyleSheetBundle>>();

            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            provider.GetNamedBundle("test");

            primer.Verify(p => p.Prime(configs), Times.Once());
        }


        [Test]
        public void Should_Not_Prime_Cache_When_Getting_Named_Bundle()
        {
            var configs = new List<IFluentConfiguration<StyleSheetBundle>>();

            configProvider.Setup(c => c.GetConfigs()).Returns(configs);
            primer.Setup(p => p.IsPrimed).Returns(true);

            provider.GetNamedBundle("test");

            primer.Verify(p => p.Prime(configs), Times.Never());
        }

        [Test]
        public void Should_Always_Prime_Cache_When_Getting_Named_Bundle()
        {
            settings.DebugMode = true;

            var configs = new List<IFluentConfiguration<StyleSheetBundle>>();

            configProvider.Setup(c => c.GetConfigs()).Returns(configs);
            primer.Setup(p => p.IsPrimed).Returns(true);

            provider.GetNamedBundle("test");
            provider.GetNamedBundle("test");
            provider.GetNamedBundle("test");

            primer.Verify(p => p.Prime(configs), Times.Exactly(3));
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            assetProvider.Setup(p => p.GetAsset("~/file.tst.tst")).Returns(new AssetBaseImpl());

            var bundle = provider.GetSourceBundle("~/file.tst.tst");            

            pipeline.Verify(p => p.Process(It.IsAny<StyleSheetBundle>()), Times.Once());
            cache.Verify(c => c.Add(bundle), Times.Once());

            Assert.IsInstanceOf<AssetBaseImpl>(bundle.Assets[0]);
            Assert.IsNotNull(bundle);
            Assert.AreEqual("5294038eea5f8cda328850bbba436881-file-tst", bundle.Name);
        }

        [Test]
        public void Should_Get_Bundle_By_Source_From_Cache()
        {
            var bundle = new StyleSheetBundle();
            bundle.Name = "199b18f549a41c8d45fe0a5b526ac060-file";

            cache.Setup(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file")).Returns(bundle);

            bundle = provider.GetSourceBundle("~/file.tst");

            pipeline.Verify(p => p.Process(bundle), Times.Never());
            cache.Verify(c => c.Add(bundle), Times.Never());
            Assert.IsNotNull(bundle);
            Assert.AreEqual("199b18f549a41c8d45fe0a5b526ac060-file", bundle.Name);
        }

        [Test]
        public void Should_Always_Load_Asset_When_Debug_Mode()
        {
            settings.DebugMode = true;

            var bundle = new StyleSheetBundle();
            bundle.Name = "199b18f549a41c8d45fe0a5b526ac060-file";

            cache.Setup(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file")).Returns(bundle);

            var bundleOut = provider.GetSourceBundle("~/file.tst");

            pipeline.Verify(p => p.Process(It.IsAny<StyleSheetBundle>()), Times.Once());
            cache.Verify(c => c.Add(It.IsAny<StyleSheetBundle>()), Times.Once());
            cache.Verify(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file"), Times.Once());
            Assert.IsNotNull(bundle);
            Assert.AreEqual("199b18f549a41c8d45fe0a5b526ac060-file", bundle.Name);
        }
    }
}
