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
    using NUnit.Framework;
    using Moq;
    using System.Web.Mvc;
    using System.Web;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Web.UI;

    [TestFixture]
    public class ScriptBundlerTests
    {
        private Mock<IBundleRenderer<ScriptBundle>> renderer;
        private ScriptBundler bundler;
        private Mock<IBundleProvider<ScriptBundle>> bundleProvider;
        private Mock<IBundleDependencyResolver<ScriptBundle>> resolver;

        [SetUp]
        public void Setup()
        {
            bundleProvider = new Mock<IBundleProvider<ScriptBundle>>();

            var collection = new BundleCollection<ScriptBundle>();
            renderer = new Mock<IBundleRenderer<ScriptBundle>>();
            resolver = new Mock<IBundleDependencyResolver<ScriptBundle>>();

            bundler = new ScriptBundler(
                bundleProvider.Object,
                renderer.Object,
                resolver.Object);
        }

        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new ScriptBundle();
            var bundles = new List<ScriptBundle>() 
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
            renderer.Verify(t => t.RenderAll(bundles, It.IsAny<BundlerState>()), Times.Once());
            resolver.Verify(t => t.Resolve(bundle));
        }

        [Test]
        public void Should_Build_And_Render_Bundle()
        {
            var bundle = new ScriptBundle();
            var bundles = new List<ScriptBundle>() 
            {
                bundle
            };

            bundleProvider.Setup(p => p.GetNamedBundle("test"))
                .Returns(bundle);

            renderer.Setup(r => r.RenderAll(bundles, It.IsAny<BundlerState>()))
                .Returns(new HtmlString(""));

            resolver.Setup(r => r.Resolve(bundle))
                .Returns(bundles);

            var htmlString = bundler.Render("test", b => b.AddAttribute("test", "test"));

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(t => t.RenderAll(bundles, It.IsAny<BundlerState>()), Times.Once());
            resolver.Verify(t => t.Resolve(bundle));
            Assert.AreEqual("test", bundle.Attributes["test"]);
        }

        [Test]
        public void Should_Include_Bundle()
        {
            var bundle = new ScriptBundle();

            bundleProvider.Setup(p => p.GetSourceBundle("~/file.js")).Returns(bundle);
            renderer.Setup(r => r.Render(bundle, It.IsAny<BundlerState>())).Returns(new HtmlString(""));

            var htmlString = bundler.Include("~/file.js");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(w => w.Render(bundle, It.IsAny<BundlerState>()), Times.Once());
        }

        [Test]
        public void Should_Build_And_Include_Bundle()
        {
            var bundle = new ScriptBundle();

            bundleProvider.Setup(p => p.GetSourceBundle("~/file.js")).Returns(bundle);
            renderer.Setup(r => r.Render(bundle, It.IsAny<BundlerState>())).Returns(new HtmlString(""));

            var htmlString = bundler.Include("~/file.js", b => b.AddAttribute("test", "test"));

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(w => w.Render(bundle, It.IsAny<BundlerState>()), Times.Once());
            Assert.AreEqual("test", bundle.Attributes["test"]);
        }

        [Test]
        public void Should_Incude_External_Bundle()
        {
            var bundle = new ScriptBundle();

            bundleProvider.Setup(p => p.GetExternalBundle("http://www.google.com/file.js")).Returns(bundle);
            renderer.Setup(r => r.Render(bundle, It.IsAny<BundlerState>())).Returns(new HtmlString(""));

            var htmlString = bundler.Include("http://www.google.com/file.js");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            renderer.Verify(w => w.Render(bundle, It.IsAny<BundlerState>()), Times.Once());
            bundleProvider.Verify(p => p.GetExternalBundle("http://www.google.com/file.js"), Times.Once());
        }
    }
}
