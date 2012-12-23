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
        private ScriptBundler builder;
        private BundleContext context;
        private Mock<IBundleProvider<ScriptBundle>> bundleProvider;

        [SetUp]
        public void Setup()
        {
            bundleProvider = new Mock<IBundleProvider<ScriptBundle>>();
            context = new BundleContext();
            var collection = new BundleCollection<ScriptBundle>();
            tagWriter = new Mock<ITagWriter<ScriptBundle>>();

            builder = new ScriptBundler(
                bundleProvider.Object,
                tagWriter.Object,
                context);
        }


        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new ScriptBundle();
            bundleProvider.Setup(p => p.GetSourceBundle("test")).Returns(bundle);
            
            builder.Render("test");

            tagWriter.Verify(t => t.Write(It.IsAny<HtmlTextWriter>(), bundle, context), Times.Once());
        }
    }
}
