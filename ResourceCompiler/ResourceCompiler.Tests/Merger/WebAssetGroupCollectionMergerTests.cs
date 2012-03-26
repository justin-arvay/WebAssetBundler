namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;

    [TestFixture]
    public class WebAssetGroupCollectionMergerTests
    {
        [Test]
        public void Should_Return_Results()
        {
            var factory = new WebAssetMergerFactory(new Mock<IWebAssetReader>().Object);
            var merger = new WebAssetGroupCollectionMerger(factory);

            var groupCollection = new WebAssetGroupCollection();
            groupCollection.Add(new WebAssetGroup("test", false));

            var results = merger.Merge(groupCollection);

            Assert.GreaterOrEqual(1, results.Count);
        }
    }
}
