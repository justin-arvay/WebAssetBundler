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
    public class VersionedImagePathResolverTests
    {
        private Mock<IUrlGenerator<ImageBundle>> urlGenerator;
        private Mock<IBundlesCache<ImageBundle>> bundleCache;
        private VersionedImagePathResolver resolver;
        private SettingsContext settings;
        private Mock<IDirectory> appDirectory;

        [SetUp]
        public void Setup()
        {
            urlGenerator = new Mock<IUrlGenerator<ImageBundle>>();
            bundleCache = new Mock<IBundlesCache<ImageBundle>>();
            
            appDirectory = new Mock<IDirectory>();
            
            settings = new SettingsContext();
            settings.AppRootDirectory = appDirectory.Object;

            resolver = new VersionedImagePathResolver(settings, bundleCache.Object, urlGenerator.Object);
        }

        [Test]
        public void Should_Replace_With_Versioned_Url()
        {
            var path = "../test/test.png";
            var assetDirectory = new Mock<IDirectory>();
            var file = new FileSystemFile("/test/test.png");

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

        [Test]
        public void Should_Get_Content_Types()
        {
            Assert.Fail();
        }
    }
}
