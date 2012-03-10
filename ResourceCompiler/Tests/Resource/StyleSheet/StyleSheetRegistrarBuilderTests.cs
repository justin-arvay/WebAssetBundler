using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler.Resource.StyleSheet;
using ResourceCompiler.Resource;
using ResourceCompiler.Resolvers;

namespace Tests.Resource.StyleSheet
{
    [TestFixture]
    public class StyleSheetRegistrarBuilderTests
    {

        private StyleSheetRegistrarBuilder CreateBuilder()
        {
            var collection = new ResourceGroupCollection();
            var urlResolver = new UrlResolver();
            var resolverFactory = new ResourceResolverFactory();
            var resolver = new ResourceGroupCollectionResolver(urlResolver, resolverFactory);

            return new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(collection), TestHelper.CreateViewContext(), resolver);
        }

        [Test]
        public void Default_Group_Returns_Self_For_Chaining()
        {
            var builder = CreateBuilder();

            Assert.IsInstanceOf<StyleSheetRegistrarBuilder>(builder.DefaultGroup(g => g.ToString()));
        }

        [Test]
        public void Can_Configure_Default_Group()
        {
            var builder = CreateBuilder();

            builder.DefaultGroup(g => g.Add("test/test.js"));

            Assert.AreEqual(1, builder.Registrar.DefaultGroup.Resources.Count);
        }

        [Test]
        public void Should_Return_Html_When_Rendered()
        {

        }

        [Test]
        public void Should_Throw_Exception_When_Render_Called_More_Than_Once()
        {

        }

        [Test]
        public void Constructor_Should_Set_Registrar()
        {
            Assert.NotNull(CreateBuilder().Registrar);
        }
    }
}
