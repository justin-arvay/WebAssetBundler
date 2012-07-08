﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
        private Mock<IWebAssetGenerator> generator;
        private Mock<IWebAssetMerger> merger;
        private StyleSheetManagerBuilder builder;

        [SetUp]
        public void Setup()
        {
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new WebAssetGroupCollection();
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new Mock<IWebAssetWriter>();
            merger = new Mock<IWebAssetMerger>();
            generator = new Mock<IWebAssetGenerator>();
            tagWriter = new Mock<ITagWriter>();

            builder = new StyleSheetManagerBuilder(
                new StyleSheetManager(collection),
                collection,
                TestHelper.CreateViewContext(),
                collectionResolver,
                tagWriter.Object,
                merger.Object,
                generator.Object);
        }

        private StyleSheetManagerBuilder CreateBuilder(ViewContext context, Mock<ITagWriter> tagWriter)
        {
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new WebAssetGroupCollection();                        
            var resolverFactory = new WebAssetResolverFactory();
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);            
            var writer = new Mock<IWebAssetWriter>();
            var merger = new Mock<IWebAssetMerger>();
            var generator = new Mock<IWebAssetGenerator>();

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(collection), 
                collection,
                context, 
                collectionResolver,
                tagWriter.Object, 
                merger.Object,
                generator.Object);
        }

        private StyleSheetManagerBuilder CreateBuilder(ViewContext context)
        {
            return CreateBuilder(context, new Mock<ITagWriter>());
        }

        [Test]
        public void Default_Group_Returns_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            Assert.IsInstanceOf<StyleSheetManagerBuilder>(builder.DefaultGroup(g => g.ToString()));
        }

        [Test]
        public void StyleSheets_Return_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());
            Assert.IsInstanceOf<StyleSheetManagerBuilder>(builder.StyleSheets(s => s.ToString()));
        }

        [Test]
        public void Can_Configure_Default_Group()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.DefaultGroup(g => g.Add("test/test.css"));

            Assert.AreEqual(1, builder.Manager.DefaultGroup.Assets.Count);
        }

        [Test]
        public void Can_Configure_Style_Sheets()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.StyleSheets(style => style.AddGroup("test", group => group.ToString()));

            //there is 2 because of default group
            Assert.AreEqual(2, builder.Manager.StyleSheets.Count);
        }

        [Test]
        public void Should_Write_Tags_On_Render()
        {
            var tagWriter = new Mock<ITagWriter>();
            var builder = CreateBuilder(TestHelper.CreateViewContext(), tagWriter);

            builder.StyleSheets(style => style
                .AddGroup("test", group => group
                    .Add("~/Files/test.css")
                    .Add("~/Files/test2.css")
                    .Combine(false)));

            builder.Render();

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), It.IsAny<IList<WebAssetMergerResult>>()), Times.Exactly(1));
        }

        [Test]
        public void Should_Write_Tags_On_ToString()
        {
            var tagWriter = new Mock<ITagWriter>();
            var builder = CreateBuilder(TestHelper.CreateViewContext(), tagWriter);

            builder.StyleSheets(style => style
                .AddGroup("test", group => group
                    .Add("~/Files/test.css")
                    .Add("~/Files/test2.css")
                    .Combine(false)));

            builder.ToHtmlString();

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), It.IsAny<IList<WebAssetMergerResult>>()), Times.Exactly(1));
        }

        [Test]
        public void Should_Generate_Merged_Results_On_Render()
        {                   
            var results = new List<WebAssetMergerResult>();
            results.Add(new WebAssetMergerResult("", ""));
            results.Add(new WebAssetMergerResult("", ""));

            merger.Setup(m => m.Merge(It.IsAny<IList<ResolverResult>>())).Returns(results);

            builder.Render();

            //should call generate with 2 results passed
            generator.Verify(g => g.Generate(It.Is<IList<WebAssetMergerResult>>(r => r.Count() == 2)), Times.Once());   
        }

        [Test]
        public void Should_Generate_Merged_Results_On_ToString()
        {
            var results = new List<WebAssetMergerResult>();
            results.Add(new WebAssetMergerResult("", ""));
            results.Add(new WebAssetMergerResult("", ""));

            merger.Setup(m => m.Merge(It.IsAny<IList<ResolverResult>>())).Returns(results);

            builder.ToHtmlString();

            //should call generate with 2 results passed
            generator.Verify(g => g.Generate(It.Is<IList<WebAssetMergerResult>>(r => r.Count() == 2)), Times.Once());   
        }


        [Test]
        public void Should_Throw_Exception_When_Render_Called_More_Than_Once()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.Render();

            Assert.Throws<InvalidOperationException>(() => builder.Render());
        }

        [Test]
        public void Constructor_Should_Set_Manage()
        {
            Assert.NotNull(CreateBuilder(TestHelper.CreateViewContext()).Manager);
        }
    }
}