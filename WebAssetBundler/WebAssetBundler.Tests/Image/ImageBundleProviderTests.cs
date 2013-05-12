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
        private Mock<IBundlePipeline<ImageBundle>> pipeline;
        private SettingsContext settings;
        private Mock<IBundleFactory<ImageBundle>> bundleFactory;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            pipeline = new Mock<IBundlePipeline<ImageBundle>>();
            cache = new Mock<IBundlesCache<ImageBundle>>();
            bundleFactory = new Mock<IBundleFactory<ImageBundle>>();

            provider = new ImageBundleProvider(cache.Object, pipeline.Object, bundleFactory.Object, settings);
        }

        [Test]
        public void Should_Get_Bundle_By_Source()
        {
            var bundle = new ImageBundle("image/png");            

            bundleFactory.Setup(p => p.CreateFromSource("~/image.png")).Returns(bundle);

            ImageBundle returnBundle = provider.GetSourceBundle("~/image.png");

            pipeline.Verify(p => p.Process(bundle));
            cache.Verify(c => c.Add(returnBundle));
            Assert.IsNotNull(returnBundle);            
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
            var cachedBundle = new ImageBundle("image/png");
            var factoryBundle = new ImageBundle("image/png");

            settings.DebugMode = true;

            //should not use this bundle
            cache.Setup(c => c.Get(ImageHelper.CreateBundleName(source)))
                .Returns(cachedBundle);

            bundleFactory.Setup(f => f.CreateFromSource("~/image.png"))
                .Returns(factoryBundle);

            ImageBundle returnBundle = provider.GetSourceBundle("~/image.png");

            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()));
            cache.Verify(c => c.Add(returnBundle));
            Assert.AreNotSame(returnBundle, cachedBundle);
        }
    }
}
