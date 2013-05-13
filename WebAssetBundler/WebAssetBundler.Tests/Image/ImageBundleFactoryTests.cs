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
    public class ImageBundleFactoryTests
    {
        private ImageBundleFactory factory;
        private Mock<IAssetProvider> assetProvider;

        [SetUp]
        public void Setup()
        {
            assetProvider = new Mock<IAssetProvider>();
            factory = new ImageBundleFactory(assetProvider.Object);
        }

        [Test]
        public void Should_Create_Bundle_From_Source()
        {
            string source = "~/image.png";
            var file = new FileSystemFile("../../Files/Images/ImageBundleFactoryTests.png");
            var asset = new FileAsset(file);

            assetProvider.Setup(p => p.GetAsset(source))
                .Returns(asset);

            ImageBundle returnBundle = factory.Create(source);

            Assert.AreEqual(1, returnBundle.Assets.Count);
            Assert.AreEqual("093e4bfea387f8f7f7948f0853b29f1d-ImageBundleFactoryTests-png", returnBundle.Name);
            Assert.AreEqual("image/png", returnBundle.ContentType);
            Assert.AreEqual(187, returnBundle.Height);
            Assert.AreEqual(196, returnBundle.Width);
        }

        [Test]
        public void Should_Create_Bundle_From_Asset()
        {
            string source = "~/image.png";
            var file = new FileSystemFile("../../Files/Images/ImageBundleFactoryTests.png");
            var asset = new FileAsset(file);

            ImageBundle returnBundle = factory.Create(asset);

            Assert.AreEqual(1, returnBundle.Assets.Count);
            Assert.AreEqual("093e4bfea387f8f7f7948f0853b29f1d-ImageBundleFactoryTests-png", returnBundle.Name);
            Assert.AreEqual("image/png", returnBundle.ContentType);
            Assert.AreEqual(187, returnBundle.Height);
            Assert.AreEqual(196, returnBundle.Width);
        }
    }
}
