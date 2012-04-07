
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class WebAssetGroupCollectionResolverTests
    {
        [Test]
        public void Should_Resolve_Collection_And_Return_Results()
        {
            var factory = new WebAssetResolverFactory(new Mock<IPathResolver>().Object);
            var resolver = new WebAssetGroupCollectionResolver(factory);
            var collection = new WebAssetGroupCollection();
            var group = new WebAssetGroup("test", false);

            group.Assets.Add(new WebAsset("path/test.css"));
            collection.Add(group);

            var results = resolver.Resolve(collection);

            Assert.AreEqual(1, results.Count);
        }
    }
}