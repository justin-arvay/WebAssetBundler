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
    public class ScriptBundleProviderTests
    {
        private ScriptBundleProvider provider;
        private Mock<IScriptConfigProvider> configProvider;
        private Mock<IBundlesCache<ScriptBundle>> cache;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<ScriptBundle>> pipeline;

        [SetUp]
        public void Setup()
        {
            pipeline = new Mock<IBundlePipeline<ScriptBundle>>();
            configProvider = new Mock<IScriptConfigProvider>();
            cache = new Mock<IBundlesCache<ScriptBundle>>();
            assetProvider = new Mock<IAssetProvider>();
            provider = new ScriptBundleProvider(configProvider.Object, cache.Object, assetProvider.Object, pipeline.Object);
        }

        [Test]
        public void Should_Get_Bundles()
        {
            var config = new ScriptBundleConfigurationImpl();
            config.Name("test");

            var configs = new List<ScriptBundleConfiguration>();
            configs.Add(config);


            configProvider.Setup(c => c.GetConfigs()).Returns(configs);

            var bundle = provider.GetBundle("test");
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

            var collection = new BundleCollection<ScriptBundle>();
            collection.Add(bundle);

            cache.Setup(c => c.Get()).Returns(collection);

            Assert.AreSame(bundle, provider.GetBundle("test"));
            cache.Verify(c => c.Get(), Times.Once());
            cache.Verify(c => c.Set(It.IsAny<BundleCollection<ScriptBundle>>()), Times.Never());

        }

    }
}