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
    using System;
    using Moq;
    using NUnit.Framework;
    using System.Web;

    [TestFixture]
    public class ExpandPathProcessorTests
    {
        private ExpandPathProcessor processor;
        private StyleSheetBundle bundle;
        private Mock<IImagePathResolverProvider> resolverProvider;
        private Mock<IImagePathResolver> resolver;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext();
            resolverProvider = new Mock<IImagePathResolverProvider>();
            resolver = new Mock<IImagePathResolver>();

            resolverProvider.Setup(r => r.GetResolver(settings))
                .Returns(resolver.Object);

            bundle = new StyleSheetBundle();
            bundle.Assets.Add(new AssetBaseImpl());
            processor = new ExpandPathProcessor(settings, resolverProvider.Object);
        }

        [Test]
        public void Should_Match_Url_With_Double_Quotes()
        {
            bundle.Assets[0].Content = "url(\"/img/test.jpg\");";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Url_With_Single_Quotes()
        {
            bundle.Assets[0].Content = "url('/img/test.jpg');";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url('/img/test.jpg');", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Url_With_No_Quotes()
        {
          
            bundle.Assets[0].Content = "url(/img/test.jpg);";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url(/img/test.jpg);", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Src_With_Double_Quotes()
        {
            bundle.Assets[0].Content = "src=\"/img/test.jpg\"";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("src=\"/img/test.jpg\"", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Src_With_Single_Quotes()
        {
            bundle.Assets[0].Content = "src='/img/test.jpg'";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("src='/img/test.jpg'", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Src_With_No_Quotes()
        {
            bundle.Assets[0].Content = "src=/img/test.jpg";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("src=/img/test.jpg", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Replace_With_Versioned_Url()
        {
            var path = "../test/test.png";

            bundle.Assets[0].Content = "url(" + path + ")";
            settings.VersionCssImages = true;

            urlGenerator.Setup(u => u.Generate(It.IsAny<ImageBundle>())).Returns("/wab.axd/image/asd/img-png");

            processor.Process(bundle);

            Assert.AreEqual("url(/wab.axd/image/asd/img-png)", bundle.Assets[0].Content);
            bundlesCache.Verify(b => b.Add(It.IsAny<ImageBundle>()));
        }

        [Test]
        public void Should_Not_Replace_With_Versioned_Path_When_External()
        {

            bundle.Assets[0].Content = "url(http://www.google.com/image,jpg)";
            settings.VersionCssImages = true;

            processor.Process(bundle);

            Assert.AreEqual("url(http://www.google.com/image,jpg)", bundle.Assets[0].Content);
            bundlesCache.Verify(b => b.Add(It.IsAny<ImageBundle>()), Times.Never());
            urlGenerator.Verify(b => b.Generate(It.IsAny<ImageBundle>()), Times.Never());
        }
    }
}
