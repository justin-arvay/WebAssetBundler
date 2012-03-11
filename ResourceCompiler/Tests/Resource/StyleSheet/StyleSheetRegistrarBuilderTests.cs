﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler.Resource.StyleSheet;
using ResourceCompiler.Resource;
using ResourceCompiler.Resolvers;
using ResourceCompiler.Extensions;
using System.Web.UI;
using System.IO;
using System.Web.Mvc;

namespace Tests.Resource.StyleSheet
{
    [TestFixture]
    public class StyleSheetRegistrarBuilderTests
    {

        private StyleSheetRegistrarBuilder CreateBuilder(ViewContext context)
        {
            var collection = new ResourceGroupCollection();
            var urlResolver = new UrlResolver();
            var resolverFactory = new ResourceResolverFactory();
            var resolver = new ResourceGroupCollectionResolver(urlResolver, resolverFactory);

            return new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(collection), context, resolver);
        }

        [Test]
        public void Default_Group_Returns_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            Assert.IsInstanceOf<StyleSheetRegistrarBuilder>(builder.DefaultGroup(g => g.ToString()));
        }

        [Test]
        public void StyleSheets_Return_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());
            Assert.IsInstanceOf<StyleSheetRegistrarBuilder>(builder.StyleSheets(s => s.ToString()));
        }

        [Test]
        public void Can_Configure_Default_Group()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.DefaultGroup(g => g.Add("test/test.js"));

            Assert.AreEqual(1, builder.Registrar.DefaultGroup.Resources.Count);
        }

        [Test]
        public void Can_Configure_Style_Sheets()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.StyleSheets(style => style.AddGroup("test", group => group.ToString()));

            //there is 2 because of default group
            Assert.AreEqual(2, builder.Registrar.StyleSheets.Count);
        }

        [Test]
        public void Should_Return_Html_When_Rendered()
        {
            var context = TestHelper.CreateViewContext();
            var builder = CreateBuilder(context);

            builder.StyleSheets(style => style
                .AddGroup("test", group => group
                    .Add("~/Files/test.css")
                    .Add("~/Files/test2.css")
                    .Combine(false)));

            builder.Render();

            string output = context.Writer.ToString();

            Assert.AreEqual(2, output.CountOccurance("<link"));
        }

        [Test]
        public void Should_Throw_Exception_When_Render_Called_More_Than_Once()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.Render();

            Assert.Throws<InvalidOperationException>(() => builder.Render());
        }

        [Test]
        public void Constructor_Should_Set_Registrar()
        {
            Assert.NotNull(CreateBuilder(TestHelper.CreateViewContext()).Registrar);
        }
    }
}
