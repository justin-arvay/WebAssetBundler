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
    public class ScriptManagerBuilderTests
    {
        private Mock<ITagWriter> tagWriter;
        private Mock<IWebAssetMerger> merger;
        private ScriptManagerBuilder builder;
        private Mock<IAssetFactory> assetFactory;
        private BuilderContext context;

        [SetUp]
        public void Setup()
        {
            assetFactory = new Mock<IAssetFactory>();
            var server = new Mock<HttpServerUtilityBase>();
            context = new BuilderContext();
            context.AssetFactory = assetFactory.Object;
            var collection = new WebAssetBundleCollection();
            var collectionResolver = new Mock<IWebAssetBundleCollectionResolver>().Object;
            merger = new Mock<IWebAssetMerger>();
            tagWriter = new Mock<ITagWriter>();

            builder = new ScriptManagerBuilder(
                new ScriptManager(collection),
                TestHelper.CreateViewContext(),
                collectionResolver,
                tagWriter.Object,
                merger.Object,
                context);
        }

        [Test]
        public void Scripts_Return_Self_For_Chaining()
        {
            Assert.IsInstanceOf<ScriptManagerBuilder>(builder.Scripts(s => s.ToString()));
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

        [Test]
        public void Should_Write_Tags_On_Render()
        {
            context.AssetFactory = new AssetFactory(context);

            var results = new List<MergerResult>();
            results.Add(new MergerResult("", "", WebAssetType.None));
            merger.Setup(m => m.Merge(It.IsAny<IList<ResolverResult>>(), It.IsAny<BuilderContext>())).Returns(results);

            builder.Render();

            tagWriter.Verify(t => t.Write(It.IsAny<HtmlTextWriter>(), It.IsAny<IList<MergerResult>>(), context), Times.Once());
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
    }
}
