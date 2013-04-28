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

            var result = new PathRewriteResult
            {
                Changed = true,
                NewPath = "/newimage/test.jpg"
            };

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);

            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Source));
            Assert.IsTrue(bundle.Assets[0].Content.Contains(result.NewPath));
        }

        [Test]
        public void Should_Match_Url_With_Single_Quotes()
        {
            bundle.Assets[0].Content = "url('/img/test.jpg');";
            bundle.Url = "/a/a/a/a";

            var result = new PathRewriteResult
            {
                Changed = true,
                NewPath = "/newimage/test.jpg"
            };

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);

            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Source));
            Assert.IsTrue(bundle.Assets[0].Content.Contains(result.NewPath));
        }

        [Test]
        public void Should_Match_Url_With_No_Quotes()
        {
          
            bundle.Assets[0].Content = "url(/img/test.jpg);";
            bundle.Url = "/a/a/a/a";

            var result = new PathRewriteResult
            {
                Changed = true,
                NewPath = "/newimage/test.jpg"
            };

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);

            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Source));
            Assert.IsTrue(bundle.Assets[0].Content.Contains(result.NewPath));
        }

        [Test]
        public void Should_Match_Src_With_Double_Quotes()
        {
            bundle.Assets[0].Content = "src=\"/img/test.jpg\"";
            bundle.Url = "/a/a/a/a";

            var result = new PathRewriteResult
            {
                Changed = true,
                NewPath = "/newimage/test.jpg"
            };

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);

            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Source));
            Assert.IsTrue(bundle.Assets[0].Content.Contains(result.NewPath));
        }

        [Test]
        public void Should_Match_Src_With_Single_Quotes()
        {
            bundle.Assets[0].Content = "(src='/img/test.jpg')";
            bundle.Url = "/a/a/a/a";

            var result = new PathRewriteResult
                {
                    Changed = true,
                    NewPath = "/newimage/test.jpg"
                };

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);

            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Content));
            Assert.IsTrue(bundle.Assets[0].Content.Contains(result.NewPath));
        }

        [Test]
        public void Should_Match_Src_With_No_Quotes()
        {
            bundle.Assets[0].Content = "src=/img/test.jpg";
            bundle.Url = "/a/a/a/a";

            var result = new PathRewriteResult
            {
                Changed = true,
                NewPath = "/newimage/test.jpg"
            };

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);


            processor.Process(bundle);

            resolverProvider.Verify(r => r.GetResolver(settings));
            resolver.Verify(r => r.Resolve("/img/test.jpg", bundle.Url, bundle.Assets[0].Source));
            Assert.IsTrue(bundle.Assets[0].Content.Contains(result.NewPath));
        }

    }
}
