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
    public class StyleSheetManagerBuilderTests
    {
        private Mock<ITagWriter> tagWriter;
        private Mock<IWebAssetMerger> merger;
        private StyleSheetManagerBuilder builder;
        private Mock<IAssetFactory> assetFactory;
        private BuilderContext context;

        [SetUp]
        public void Setup()
        {
            assetFactory = new Mock<IAssetFactory>();
            context = new BuilderContext();
            context.AssetFactory = assetFactory.Object;

            var server = new Mock<HttpServerUtilityBase>();
            var collection = new WebAssetBundleCollection();
            var collectionResolver = new Mock<IWebAssetBundleCollectionResolver>().Object;
            merger = new Mock<IWebAssetMerger>();
            tagWriter = new Mock<ITagWriter>();

            builder = new StyleSheetManagerBuilder(
                new StyleSheetManager(collection),
                TestHelper.CreateViewContext(),
                collectionResolver,
                tagWriter.Object,
                merger.Object,
                context);
        }
 

        [Test]
        public void StyleSheets_Return_Self_For_Chaining()
        {
            Assert.IsInstanceOf<StyleSheetManagerBuilder>(builder.StyleSheets(s => s.ToString()));
        }



        [Test]
        public void Should_Write_Tags_On_Render()
        {
            context.AssetFactory = new AssetFactory(context);

            var results = new List<MergerResult>();
            results.Add(new MergerResult("", "", WebAssetType.None));

            builder.Render();

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), It.IsAny<IList<MergerResult>>(), context), Times.Exactly(1));
        }

        [Test]
        public void Should_Write_Tags_On_ToString()
        {
            context.AssetFactory = new AssetFactory(context);

            var results = new List<MergerResult>();
            results.Add(new MergerResult("", "", WebAssetType.None));

            builder.ToHtmlString();

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), It.IsAny<IList<MergerResult>>(), context), Times.Exactly(1));
        }      

        [Test]
        public void Should_Throw_Exception_When_Render_Called_More_Than_Once()
        {
            builder.Render();

            Assert.Throws<InvalidOperationException>(() => builder.Render());
        }

        [Test]
        public void Constructor_Should_Set_Manage()
        {
            Assert.NotNull(builder.Manager);
        }
    }
}
