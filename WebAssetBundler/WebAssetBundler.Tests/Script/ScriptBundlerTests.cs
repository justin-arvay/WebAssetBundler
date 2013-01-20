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
        private Mock<ITagWriter<ScriptBundle>> tagWriter;
        private ScriptBundler bundler;
        private BundleContext context;
        private Mock<IBundleProvider<ScriptBundle>> bundleProvider;

        [SetUp]
        public void Setup()
        {
            bundleProvider = new Mock<IBundleProvider<ScriptBundle>>();
            context = new BundleContext();
            var collection = new BundleCollection<ScriptBundle>();
            tagWriter = new Mock<ITagWriter<ScriptBundle>>();

            bundler = new ScriptBundler(
                bundleProvider.Object,
                tagWriter.Object,
                context);
        }


        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new ScriptBundle();
            bundleProvider.Setup(p => p.GetNamedBundle("test")).Returns(bundle);
            
            var htmlString = bundler.Render("test");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), bundle, context), Times.Once());
        }

        [Test]
        public void Should_Include_Bundle()
        {
            var bundle = new ScriptBundle();

            bundleProvider.Setup(p => p.GetSourceBundle("~/file.js")).Returns(bundle);

            var htmlString = bundler.Include("~/file.js");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            tagWriter.Verify(w => w.Write(It.IsAny<TextWriter>(), bundle, context), Times.Once());
        }

        [Test]
        public void Should_Incude_External_Bundle()
        {
            var bundle = new ScriptBundle();

            bundleProvider.Setup(p => p.GetExternalBundle("http://www.google.com/file.js")).Returns(bundle);

            var htmlString = bundler.Include("http://www.google.com/file.js");

            Assert.IsInstanceOf<IHtmlString>(htmlString);
            tagWriter.Verify(w => w.Write(It.IsAny<TextWriter>(), bundle, context), Times.Once());
            bundleProvider.Verify(p => p.GetExternalBundle("http://www.google.com/file.js"), Times.Once());
        }
    }
}
