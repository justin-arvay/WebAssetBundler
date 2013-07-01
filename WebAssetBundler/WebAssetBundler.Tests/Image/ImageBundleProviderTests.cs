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

    [TestFixture]
    public class ImageBundleProviderTests
    {
        private ImageBundleProvider provider;
        private Mock<IBundleCache<ImageBundle>> cache;
        private Mock<IBundlePipeline<ImageBundle>> pipeline;
        private SettingsContext settings;
        private Mock<IBundleFactory<ImageBundle>> bundleFactory;
        private Mock<IAssetProvider> assetProvider;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            pipeline = new Mock<IBundlePipeline<ImageBundle>>();
            cache = new Mock<IBundleCache<ImageBundle>>();
            bundleFactory = new Mock<IBundleFactory<ImageBundle>>();
            assetProvider = new Mock<IAssetProvider>();

            provider = new ImageBundleProvider(cache.Object, pipeline.Object, bundleFactory.Object, assetProvider.Object, settings);
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            var bundle = new ImageBundle("image/png");
            var asset = new AssetBaseImpl();
            asset.Source = "~/image.png";

            assetProvider.Setup(a => a.GetAsset(asset.Source))
                .Returns(asset);

            bundleFactory.Setup(p => p.Create(asset)).Returns(bundle);

            ImageBundle returnBundle = provider.GetSourceBundle("~/image.png");

            pipeline.Verify(p => p.Process(bundle));
            cache.Verify(c => c.Add(returnBundle));
            Assert.IsNotNull(returnBundle);            
        }

        [Test]
        public void Should_Get_Bundle_By_Source_And_From_Cache()
        {
            var bundle = new ImageBundle("image/png");
            var asset = new AssetBaseImpl();
            asset.Source = "~/image.png";

            assetProvider.Setup(a => a.GetAsset(asset.Source))
                .Returns(asset);

            cache.Setup(c => c.Get(ImageHelper.CreateBundleName(asset)))
                .Returns(bundle);

            ImageBundle returnBundle = provider.GetSourceBundle(asset.Source);

            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()), Times.Never());
            cache.Verify(c => c.Add(bundle), Times.Never());
            Assert.AreSame(returnBundle, bundle);
        }

        [Test]
        public void Should_Always_Get_Bundle_By_Source_When_In_Debug()
        {
            var cachedBundle = new ImageBundle("image/png");
            var factoryBundle = new ImageBundle("image/png");
            var asset = new AssetBaseImpl();
            asset.Source = "~/image.png";

            assetProvider.Setup(a => a.GetAsset(asset.Source))
                .Returns(asset);

            settings.DebugMode = true;

            //should not use this bundle
            cache.Setup(c => c.Get(ImageHelper.CreateBundleName(asset)))
                .Returns(cachedBundle);

            bundleFactory.Setup(f => f.Create(asset))
                .Returns(factoryBundle);

            ImageBundle returnBundle = provider.GetSourceBundle("~/image.png");

            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()));
            cache.Verify(c => c.Add(returnBundle));
            Assert.AreNotSame(returnBundle, cachedBundle);
        }
    }
}
