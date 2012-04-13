
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
            //var writer = new Mock<IWebAssetWriter>();
            //var merger = new ScriptWebAssetMerger(new Mock<IWebAssetReader>().Object);
            var generator = new Mock<IWebAssetGenerator>();

            return new ScriptManagerBuilder(
                new ScriptManager(collection), 
                context, 
                collectionResolver, 
                urlResolver.Object,                 
                cacheFactory.Object, 
                generator.Object);
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

        [Test]
        public void Can_Configure_Default_Group()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.DefaultGroup(g => g.Add("test/test.js"));

            Assert.AreEqual(1, builder.Manager.DefaultGroup.Assets.Count);
        }

        [Test]
        public void Can_Configure_Scripts()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.Scripts(s => s.AddGroup("test", group => group.ToString()));

            //there is 2 because of default group
            Assert.AreEqual(2, builder.Manager.Scripts.Count);
        }
    }
}
