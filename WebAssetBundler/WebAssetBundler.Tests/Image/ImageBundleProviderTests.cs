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
        private Mock<IBundlesCache<ImageBundle>> cache;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IBundlePipeline<ImageBundle>> pipeline;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            pipeline = new Mock<IBundlePipeline<ImageBundle>>();
            cache = new Mock<IBundlesCache<ImageBundle>>();
            assetProvider = new Mock<IAssetProvider>();

            provider = new ImageBundleProvider(cache.Object, assetProvider.Object,
                pipeline.Object, settings);
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            assetProvider.Setup(p => p.GetAsset("~/image.png")).Returns(new AssetBaseImpl());

            ImageBundle bundle = provider.GetSourceBundle("~/image.png");

            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()), Times.Once());
            cache.Verify(c => c.Add(bundle), Times.Once());
            Assert.IsInstanceOf<AssetBaseImpl>(bundle.Assets[0]);
            Assert.IsNotNull(bundle);
            Assert.AreEqual("4c761f170e016836ff84498202b99827-image-png", bundle.Name);
        }

        [Test]
        public void Should_Get_Bundle_By_Source_And_From_Cache()
        {
            string source = "~/image.png";
            var bundle = new ImageBundle("image/png");

            cache.Setup(c => c.Get(ImageHelper.CreateBundleName(source)))
                .Returns(bundle);

            ImageBundle returnBundle = provider.GetSourceBundle(source);

            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()), Times.Never());
            cache.Verify(c => c.Add(bundle), Times.Never());
            Assert.AreSame(returnBundle, bundle);
        }

        [Test]
        public void Should_Always_Get_Bundle_By_Source_When_In_Debug()
        {
            string source = "~/image.png";
            var bundle = new ImageBundle("image/png");

            settings.DebugMode = true;

            //should not use this bundle
            cache.Setup(c => c.Get(ImageHelper.CreateBundleName(source)))
                .Returns(bundle);

            assetProvider.Setup(p => p.GetAsset("~/image.png"))
                .Returns(new AssetBaseImpl());

            ImageBundle returnBundle = provider.GetSourceBundle("~/image.png");

            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()), Times.Once());
            cache.Verify(c => c.Add(returnBundle), Times.Once());
            Assert.IsInstanceOf<AssetBaseImpl>(returnBundle.Assets[0]);
            Assert.IsNotNull(returnBundle);
            Assert.AreEqual("4c761f170e016836ff84498202b99827-image-png", returnBundle.Name);
            Assert.AreNotSame(returnBundle, bundle);
        }
    }
}
