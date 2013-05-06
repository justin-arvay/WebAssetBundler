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
    public class ImageProcessorTests
    {
        private Mock<IBundlePipeline<ImageBundle>> pipeline;
        private ImageProcessor processor;
        private Mock<IBundlesCache<ImageBundle>> bundlesCache;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext();
            bundlesCache = new Mock<IBundlesCache<ImageBundle>>();
            pipeline = new Mock<IBundlePipeline<ImageBundle>>();
            processor = new ImageProcessor(pipeline.Object, bundlesCache.Object, settings);
        }

        [Test]
        public void Should_Process_Image_Bundle()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_Replace_With_Versioned_Url()
        {
            var path = "../test/test.png";
            var assetDirectory = new Mock<IDirectory>();
            var file = new FileSystemFile(AppDomain.CurrentDomain.BaseDirectory + "/../../Files/Images/VersionImageTest.png");

            urlGenerator.Setup(u => u.Generate(It.IsAny<ImageBundle>()))
                .Returns("/wab.axd/image/asd/img-png");

            appDirectory.Setup(a => a.GetDirectory(It.IsAny<string>()))
                .Returns(assetDirectory.Object);

            assetDirectory.Setup(a => a.GetFile(It.IsAny<string>()))
                .Returns(file);

            var result = resolver.Resolve(path, null, "/Content/file.css");

            Assert.AreEqual("/wab.axd/image/asd/img-png", result.NewPath);
            bundleCache.Verify(b => b.Add(It.IsAny<ImageBundle>()));
            urlGenerator.Verify(u => u.Generate(It.IsAny<ImageBundle>()));
            Assert.IsTrue(result.Changed);
        }

        [Test]
        public void Should_Not_Replace_With_Versioned_Path_When_Http()
        {
            var path = "http://www.google.com/image,jpg";

            var result = resolver.Resolve(path, null, "/Content/file.css");

            Assert.AreEqual(null, result.NewPath);
            bundleCache.Verify(b => b.Add(It.IsAny<ImageBundle>()), Times.Never());
            urlGenerator.Verify(u => u.Generate(It.IsAny<ImageBundle>()), Times.Never());
            Assert.IsFalse(result.Changed);
        }

        [Test]
        public void Should_Not_Replace_With_Versioned_Path_When_Https()
        {
            var path = "https://www.google.com/image,jpg";

            var result = resolver.Resolve(path, null, "/Content/file.css");

            Assert.AreEqual(null, result.NewPath);
            bundleCache.Verify(b => b.Add(It.IsAny<ImageBundle>()), Times.Never());
            urlGenerator.Verify(u => u.Generate(It.IsAny<ImageBundle>()), Times.Never());
            Assert.IsFalse(result.Changed);
        }
    }
}
