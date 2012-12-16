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
        private Mock<ITagWriter<StyleSheetBundle>> tagWriter;
        private StyleSheetBundler builder;
        private BundleContext context;
        private Mock<IBundleProvider<StyleSheetBundle>> bundleProvider;

        [SetUp]
        public void Setup()
        {
            bundleProvider = new Mock<IBundleProvider<StyleSheetBundle>>();
            context = new BundleContext();
            tagWriter = new Mock<ITagWriter<StyleSheetBundle>>();

            builder = new StyleSheetBundler(
                bundleProvider.Object,
                tagWriter.Object,
                context);
        }
 

        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new StyleSheetBundle();
            bundleProvider.Setup(p => p.GetBundle("test")).Returns(bundle);
            
            builder.Render("test");

            tagWriter.Verify(t => t.Write(It.IsAny<HtmlTextWriter>(), bundle, context), Times.Once());
        }

       
    }
}
