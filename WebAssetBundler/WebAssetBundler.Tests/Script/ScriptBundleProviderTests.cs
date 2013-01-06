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

    [TestFixture]
    public class ScriptBundleProviderTests
    {
        private ScriptBundleProvider provider;
        private Mock<IScriptConfigProvider> configProvider;
        private Mock<IBundlesCache<ScriptBundle>> cache;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<ScriptBundle>> pipeline;
        private Mock<HttpServerUtilityBase> server;
        private BundleContext context;

        [SetUp]
        public void Setup()
        {
            context = new BundleContext();
            pipeline = new Mock<IBundlePipeline<ScriptBundle>>();
            configProvider = new Mock<IScriptConfigProvider>();
            cache = new Mock<IBundlesCache<ScriptBundle>>();
            assetProvider = new Mock<IAssetProvider>();
            server = new Mock<HttpServerUtilityBase>();
            provider = new ScriptBundleProvider(configProvider.Object, cache.Object, assetProvider.Object, pipeline.Object, server.Object, context);
        }

        [Test]
        public void Should_Get_Bundles()
        {
            var config = new ScriptBundleConfigurationImpl();
            config.Name("test");

            var configs = new List<ScriptBundleConfiguration>();
            configs.Add(config);


            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            var bundle = provider.GetNamedBundle("test");
            Assert.AreSame(config.GetBundle(), bundle);
            Assert.AreEqual(1, config.CallCount);
            Assert.IsInstanceOf<IAssetProvider>(config.AssetProvider);
            cache.Verify(c => c.Get(), Times.Once());
            cache.Verify(c => c.Set(It.IsAny<BundleCollection<ScriptBundle>>()), Times.Once());
        }

        [Test]
        public void Should_Get_Bundles_From_Cache()
        {
            var bundle = new ScriptBundle();
            bundle.Name = "test";

            var collection = new BundleCollection<ScriptBundle>();
            collection.Add(bundle);

            cache.Setup(c => c.Get()).Returns(collection);

            Assert.AreSame(bundle, provider.GetNamedBundle("test"));
            cache.Verify(c => c.Get(), Times.Once());
            cache.Verify(c => c.Set(It.IsAny<BundleCollection<ScriptBundle>>()), Times.Never());

        }

        [Test]
        public void Should_Always_Read_From_Config_When_Debug_Mode()
        {
            context.DebugMode = true;

            var config = new ScriptBundleConfigurationImpl();
            config.Name("test");

            var configs = new List<ScriptBundleConfiguration>();
            configs.Add(config);


            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            var bundle = new ScriptBundle();
            bundle.Name = "test";

            var collection = new BundleCollection<ScriptBundle>();
            collection.Add(bundle);

            cache.Setup(c => c.Get()).Returns(collection);

            Assert.IsInstanceOf<ScriptBundle>(provider.GetNamedBundle("test"));
            cache.Verify(c => c.Get(), Times.Once());
            cache.Verify(c => c.Set(It.IsAny<BundleCollection<ScriptBundle>>()), Times.Once());
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            var bundle = provider.GetSourceBundle("~/file.tst.tst");

            pipeline.Verify(p => p.Process(It.IsAny<ScriptBundle>()), Times.Once());
            cache.Verify(c => c.Add(bundle), Times.Once());
            Assert.IsNotNull(bundle);
            Assert.AreEqual("5294038eea5f8cda328850bbba436881-file-tst", bundle.Name);
        }

        [Test]
        public void Should_Get_Bundle_By_Source_From_Cache()
        {
            var bundle = new ScriptBundle();
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

            var bundle = new ScriptBundle();
            bundle.Name = "199b18f549a41c8d45fe0a5b526ac060-file";

            cache.Setup(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file")).Returns(bundle);

            var bundleOut = provider.GetSourceBundle("~/file.tst");

            pipeline.Verify(p => p.Process(It.IsAny<ScriptBundle>()), Times.Once());
            cache.Verify(c => c.Add(It.IsAny<ScriptBundle>()), Times.Once());
            cache.Verify(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file"), Times.Once());
            Assert.IsNotNull(bundle);
            Assert.AreEqual("199b18f549a41c8d45fe0a5b526ac060-file", bundle.Name);
        }

    }
}
