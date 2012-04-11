namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;
    using System.Web.UI;
    using System.IO;
    using System.Web.Mvc;
    using Moq;
    using System.Web;


    [TestFixture]
    public class StyleSheetManagerBuilderTests
    {

        private StyleSheetManagerBuilder CreateBuilder(ViewContext context, Mock<IUrlResolver> urlResolver)
        {
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new WebAssetGroupCollection();            
            var pathResolver = new Mock<IPathResolver>();
            var resolverFactory = new WebAssetResolverFactory(pathResolver.Object);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);            
            var cacheFactory = new Mock<ICacheFactory>();
            var writer = new Mock<IWebAssetWriter>();
            var merger = new StyleSheetWebAssetMerger(new Mock<IWebAssetReader>().Object, new Mock<IWebAssetContentFilter>().Object, server.Object);

            return new StyleSheetManagerBuilder(new StyleSheetManager(collection), context, collectionResolver, urlResolver.Object, writer.Object, cacheFactory.Object, merger);
        }

        private StyleSheetManagerBuilder CreateBuilder(ViewContext context)
        {
            return CreateBuilder(context, new Mock<IUrlResolver>());
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

            builder.DefaultGroup(g => g.Add("test/test.js"));

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
        public void Should_Resole_Virtual_Path_To_Url()
        {
            var urlResolver = new Mock<IUrlResolver>();
            var builder = CreateBuilder(TestHelper.CreateViewContext(), urlResolver);
            var path = "~/Files/test/css";

            builder.StyleSheets(style => style
                .AddGroup("test", group => group
                    .Add(path)
                    .Combine(false)));            

            builder.Render();

            urlResolver.Verify(m => m.Resolve(It.Is<string>(s => s.Equals(path))), Times.Exactly(1));
            
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
            Assert.NotNull(CreateBuilder(TestHelper.CreateViewContext()).Manager);
        }
    }
}
