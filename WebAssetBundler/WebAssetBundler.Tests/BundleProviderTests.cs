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
    public class BundleProviderTests
    {
        private BundleProvider<BundleImpl> provider;
        private Mock<IBundleCache<BundleImpl>> cache;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<BundleImpl>> pipeline;
        private Mock<IConfigurationDriver> driver;
        private Mock<IBundleFactory<BundleImpl>> factory;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            pipeline = new Mock<IBundlePipeline<BundleImpl>>();
            cache = new Mock<IBundleCache<BundleImpl>>();
            assetProvider = new Mock<IAssetProvider>();
            driver = new Mock<IConfigurationDriver>();
            factory = new Mock<IBundleFactory<BundleImpl>>();

            provider = new BundleProvider<BundleImpl>(cache.Object, factory.Object, driver.Object, assetProvider.Object, pipeline.Object,
                settings);
        }

        [Test]
        public void Should_Get_Named_Bundle()
        {
            var bundle = new BundleImpl();
            cache.Setup(c => c.Get("test")).Returns(bundle);

            var bundleOut = provider.GetNamedBundle("test");

            cache.Verify(c => c.Get("test"), Times.Once());
            Assert.AreSame(bundleOut, bundle);
        }

        [Test]
        public void Should_Always_Cache_When_Getting_Named_Bundle()
        {
            settings.DebugMode = true;

            var bundle = new BundleImpl();
            cache.Setup(c => c.Get("test")).Returns(bundle);

            provider.GetNamedBundle("test");
            provider.GetNamedBundle("test");
            provider.GetNamedBundle("test");

            cache.Verify(c => c.Get("test"), Times.Exactly(3));           
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            var bundle = new BundleImpl();
            bundle.Assets.Add(new AssetBaseImpl());

            assetProvider.Setup(p => p.GetAsset("~/file.tst"))
                .Returns(bundle.Assets[0]);

            factory.Setup(f => f.Create(bundle.Assets[0]))
                .Returns(bundle);

            var returnBundle = provider.GetSourceBundle("~/file.tst");

            pipeline.Verify(p => p.Process(It.IsAny<BundleImpl>()), Times.Once());
            cache.Verify(c => c.Add(bundle), Times.Once());

            Assert.IsNotNull(returnBundle);
        }

        [Test]
        public void Should_Get_Bundle_By_Source_From_Cache()
        {
            var bundle = new BundleImpl();
            bundle.Name = "199b18f549a41c8d45fe0a5b526ac060-file-tst";

            cache.Setup(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file-tst")).Returns(bundle);

            bundle = provider.GetSourceBundle("~/file.tst");

            pipeline.Verify(p => p.Process(bundle), Times.Never());
            cache.Verify(c => c.Add(bundle), Times.Never());
            Assert.IsNotNull(bundle);
            Assert.AreEqual("199b18f549a41c8d45fe0a5b526ac060-file-tst", bundle.Name);
        }

        [Test]
        public void Should_Always_Load_Asset_When_Debug_Mode()
        {
            settings.DebugMode = true;

            var bundle = new BundleImpl();
            bundle.Name = "199b18f549a41c8d45fe0a5b526ac060-file-tst";

            cache.Setup(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file-tst")).Returns(bundle);

            var bundleOut = provider.GetSourceBundle("~/file.tst");

            pipeline.Verify(p => p.Process(It.IsAny<BundleImpl>()), Times.Once());
            cache.Verify(c => c.Add(It.IsAny<BundleImpl>()), Times.Once());
            cache.Verify(c => c.Get("199b18f549a41c8d45fe0a5b526ac060-file-tst"), Times.Once());
            Assert.IsNotNull(bundle);
            Assert.AreEqual("199b18f549a41c8d45fe0a5b526ac060-file-tst", bundle.Name);
        }
    }
}
