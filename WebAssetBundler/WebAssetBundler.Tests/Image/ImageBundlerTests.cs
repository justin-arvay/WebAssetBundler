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
    using System.Web;
    using System.IO;

    [TestFixture]
    public class ImageBundlerTests
    {
        private Mock<ITagWriter<ImageBundle>> tagWriter;
        private ImageBundler bundler;
        private Mock<IBundleProvider<ImageBundle>> bundleProvider;

        [SetUp]
        public void Setup()
        {
            bundleProvider = new Mock<IBundleProvider<ImageBundle>>();
            tagWriter = new Mock<ITagWriter<ImageBundle>>();

            bundler = new ImageBundler(
                bundleProvider.Object,
                tagWriter.Object);
        }

        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new ImageBundle("image/png");
            string source = "~/image.png";

            bundleProvider.Setup(p => p.GetSourceBundle(source)).Returns(bundle);

            IHtmlString htmlString = bundler.Render(source);

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), bundle), Times.Once());
        }

        [Test]
        public void Should_Build_And_Render_Bundle()
        {
            var bundle = new ImageBundle("image/png");
            string source = "~/image.png";

            bundleProvider.Setup(p => p.GetSourceBundle(source))
                .Returns(bundle);

            IHtmlString htmlString = bundler.Render(source, b => b.Alt("test alt"));

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            Assert.AreEqual("test alt", bundle.Alt);
            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), bundle), Times.Once());
        }
    }
}
