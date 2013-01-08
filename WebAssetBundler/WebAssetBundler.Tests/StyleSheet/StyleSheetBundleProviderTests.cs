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
    using System.Web;
    using System.IO;

    [TestFixture]
    public class StyleSheetBundleProviderTests
    {
        private StyleSheetBundleProvider provider;
        private Mock<IStyleSheetConfigProvider> configProvider;
        private Mock<IBundlesCache<StyleSheetBundle>> cache;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<StyleSheetBundle>> pipeline;
        private Mock<HttpServerUtilityBase> server;
        private BundleContext context;

        [SetUp]
        public void Setup()
        {
            context = new BundleContext();
            pipeline = new Mock<IBundlePipeline<StyleSheetBundle>>();
            configProvider = new Mock<IStyleSheetConfigProvider>();
            cache = new Mock<IBundlesCache<StyleSheetBundle>>();
            assetProvider = new Mock<IAssetProvider>();
            server = new Mock<HttpServerUtilityBase>();
            provider = new StyleSheetBundleProvider(configProvider.Object, cache.Object, pipeline.Object, assetProvider.Object, server.Object, context);
        }

        [Test]
        public void Should_Get_Bundle()
        {
            var config = new StyleSheetBundleConfigurationImpl();
            config.Name("test");

            var configs = new List<StyleSheetBundleConfiguration>();
            configs.Add(config);


            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            var bundle = provider.GetNamedBundle("test");

            Assert.AreSame(config.GetBundle(), bundle);
            Assert.AreEqual(1, config.CallCount);
            Assert.IsInstanceOf<IAssetProvider>(config.AssetProvider);
            cache.Verify(c => c.Get("test"), Times.AtLeast(1));
            cache.Verify(c => c.Add(It.IsAny<StyleSheetBundle>()), Times.Once());
        }

        [Test]
        public void Should_Get_Bundle_From_Cache()
        {
            var bundle = new StyleSheetBundle();
            bundle.Name = "test";

            cache.Setup(c => c.Get(bundle.Name)).Returns(bundle);

            Assert.AreSame(bundle, provider.GetNamedBundle("test"));
            cache.Verify(c => c.Get(bundle.Name), Times.Once());
            cache.Verify(c => c.Add(It.IsAny<StyleSheetBundle>()), Times.Never());
        }

        [Test]
        public void Should_Always_Read_From_Config_When_Debug_Mode()
        {
            context.DebugMode = true;

            var config = new StyleSheetBundleConfigurationImpl();
            config.Name("test");

            var configs = new List<StyleSheetBundleConfiguration>();
            configs.Add(config);


            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            var bundle = new StyleSheetBundle();
            bundle.Name = "test";

            cache.Setup(c => c.Get(bundle.Name)).Returns(bundle);

            Assert.IsInstanceOf<StyleSheetBundle>(provider.GetNamedBundle("test"));
            cache.Verify(c => c.Get(bundle.Name), Times.AtLeast(1));
            cache.Verify(c => c.Add(It.IsAny<StyleSheetBundle>()), Times.Once());
        }

        [Test]
        public void Should_Prime_Cache()
        {
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            var bundle = provider.GetSourceBundle("~/file.tst.tst");

            pipeline.Verify(p => p.Process(It.IsAny<StyleSheetBundle>()), Times.Once());
            cache.Verify(c => c.Add(bundle), Times.Once());
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
            context.DebugMode = true;

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
