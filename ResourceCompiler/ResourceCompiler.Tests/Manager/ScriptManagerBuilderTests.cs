
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Web.Mvc;
    using System.Web;

    [TestFixture]
    public class ScriptManagerBuilderTests
    {

        private ScriptManagerBuilder CreateBuilder(ViewContext context, Mock<IUrlResolver> urlResolver)
        {
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new WebAssetGroupCollection();
            var pathResolver = new Mock<IPathResolver>();
            var resolverFactory = new WebAssetResolverFactory(pathResolver.Object);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var cacheFactory = new Mock<ICacheFactory>();
            var writer = new Mock<IWebAssetWriter>();
            var merger = new ScriptWebAssetMerger(new Mock<IWebAssetReader>().Object);

            return new ScriptManagerBuilder(new ScriptManager(collection), context, collectionResolver, urlResolver.Object, writer.Object, cacheFactory.Object, merger);
        }

        private ScriptManagerBuilder CreateBuilder(ViewContext context)
        {
            return CreateBuilder(context, new Mock<IUrlResolver>());
        }

        [Test]
        public void Default_Group_Returns_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            Assert.IsInstanceOf<ScriptManagerBuilder>(builder.DefaultGroup(g => g.ToString()));
        }

        [Test]
        public void Scripts_Return_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());
            Assert.IsInstanceOf<ScriptManagerBuilder>(builder.Scripts(s => s.ToString()));
        }
    }
}
