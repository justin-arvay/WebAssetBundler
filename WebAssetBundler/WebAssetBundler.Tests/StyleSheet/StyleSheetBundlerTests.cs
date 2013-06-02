// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using WebAssetBundler.Web.Mvc;
    using System.Web.UI;
    using System.IO;
    using System.Web.Mvc;
    using Moq;
    using System.Web;


    [TestFixture]
    public class StyleSheetBundlerTests
    {
        private Mock<IBundleRenderer<StyleSheetBundle>> renderer;
        private StyleSheetBundler bundler;
        private Mock<IBundleProvider<StyleSheetBundle>> bundleProvider;
        private Mock<IBundleDependencyResolver<StyleSheetBundle>> resolver;

        [SetUp]
        public void Setup()
        {
            bundleProvider = new Mock<IBundleProvider<StyleSheetBundle>>();
            renderer = new Mock<IBundleRenderer<StyleSheetBundle>>();
            resolver = new Mock<IBundleDependencyResolver<StyleSheetBundle>>();

            bundler = new StyleSheetBundler(
                bundleProvider.Object,
                renderer.Object,
                resolver.Object);
        }

        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new StyleSheetBundle();
            var bundles = new List<StyleSheetBundle>() 
            {
                bundle
            };

            bundleProvider.Setup(p => p.GetNamedBundle("test"))
                .Returns(bundle);

            renderer.Setup(r => r.RenderAll(bundles, It.IsAny<BundlerState>()))
                .Returns(new HtmlString(""));

            resolver.Setup(r => r.Resolve(bundle))
                .Returns(bundles);

            var htmlString = bundler.Render("test");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(t => t.RenderAll(bundles, It.IsAny<BundlerState>()));
            resolver.Verify(t => t.Resolve(bundle));
        }

        [Test]
        public void Should_Build_And_Render_Bundle()
        {
            var bundle = new StyleSheetBundle();
            var bundles = new List<StyleSheetBundle>() 
            {
                bundle
            };

            bundleProvider.Setup(p => p.GetNamedBundle("test"))
                .Returns(bundle);

            renderer.Setup(r => r.RenderAll(bundles, It.IsAny<BundlerState>()))
               .Returns(new HtmlString(""));

            resolver.Setup(r => r.Resolve(bundle))
                .Returns(bundles);

            var htmlString = bundler.Render("test", b => b
                .AddAttribute("test", "test"));

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(t => t.RenderAll(bundles, It.IsAny<BundlerState>()), Times.Once());
            resolver.Verify(t => t.Resolve(bundle));
            Assert.AreEqual("test", bundle.Attributes["test"]);
        }

        [Test]
        public void Should_Include_Bundle()
        {
            var bundle = new StyleSheetBundle();

            bundleProvider.Setup(p => p.GetSourceBundle("~/file.css")).Returns(bundle);
            renderer.Setup(r => r.Render(bundle, It.IsAny<BundlerState>())).Returns(new HtmlString(""));

            var htmlString = bundler.Include("~/file.css");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(w => w.Render(bundle, It.IsAny<BundlerState>()), Times.Once());
        }

        [Test]
        public void Should_Build_And_Include_Bundle()
        {
            var bundle = new StyleSheetBundle();

            bundleProvider.Setup(p => p.GetSourceBundle("~/file.css")).Returns(bundle);
            renderer.Setup(r => r.Render(bundle, It.IsAny<BundlerState>())).Returns(new HtmlString(""));

            var htmlString = bundler.Include("~/file.css", b => b
                .AddAttribute("test", "test"));

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(w => w.Render(bundle, It.IsAny<BundlerState>()), Times.Once());
            Assert.AreEqual("test", bundle.Attributes["test"]);
        }

        [Test]
        public void Should_Include_External_Bundle()
        {
            var bundle = new StyleSheetBundle();

            bundleProvider.Setup(p => p.GetExternalBundle("http://www.google.com/file.css")).Returns(bundle);
            renderer.Setup(r => r.Render(bundle, It.IsAny<BundlerState>())).Returns(new HtmlString(""));

            var htmlString = bundler.Include("http://www.google.com/file.css");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(w => w.Render(bundle, It.IsAny<BundlerState>()), Times.Once());
            bundleProvider.Verify(p => p.GetExternalBundle("http://www.google.com/file.css"), Times.Once());
        }
    }
}
