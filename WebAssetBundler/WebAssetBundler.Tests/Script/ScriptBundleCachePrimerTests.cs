
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
    public class ScriptBundleCachePrimerTests
    {
        private ScriptBundleCachePrimer primer;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<ScriptBundle>> pipeline;
        private Mock<IBundlesCache<ScriptBundle>> cache;

        [SetUp]
        public void Setup()
        {
            assetProvider = new Mock<IAssetProvider>();
            pipeline = new Mock<IBundlePipeline<ScriptBundle>>();
            cache = new Mock<IBundlesCache<ScriptBundle>>();
            primer = new ScriptBundleCachePrimer(assetProvider.Object, pipeline.Object, cache.Object);
        }

        [Test]
        public void Should_Be_Primed()
        {
            primer.Prime(new List<IBundleConfiguration<ScriptBundle>>());

            Assert.IsTrue(primer.IsPrimed);
        }

        [Test]
        public void Should_Prime_Cache()
        {
            var configOne = new ScriptBundleConfigurationImpl();
            var configTwo = new ScriptBundleConfigurationImpl();

            var configs = new List<IBundleConfiguration<ScriptBundle>>();
            configs.Add(configOne);
            configs.Add(configTwo);

            primer.Prime(configs);

            cache.Verify(c => c.Add(It.IsAny<ScriptBundle>()), Times.Exactly(2));
            pipeline.Verify(p => p.Process(It.IsAny<ScriptBundle>()), Times.Exactly(2));
            Assert.AreEqual(1, configOne.CallCount);
            Assert.AreEqual(1, configTwo.CallCount);
            Assert.IsInstanceOf<IAssetProvider>(configOne.AssetProvider);
            Assert.IsInstanceOf<IAssetProvider>(configTwo.AssetProvider);
            Assert.IsInstanceOf<ScriptBundle>(configOne.Bundle);
            Assert.IsInstanceOf<ScriptBundle>(configTwo.Bundle);

        }

        [Test]
        public void Should_Not_Prime_Cache()
        {

            primer.Prime(new List<IBundleConfiguration<ScriptBundle>>());

            cache.Verify(c => c.Add(It.IsAny<ScriptBundle>()), Times.Never());
        }
    }
}
