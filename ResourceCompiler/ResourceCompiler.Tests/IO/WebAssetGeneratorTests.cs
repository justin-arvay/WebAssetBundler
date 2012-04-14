
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class WebAssetGeneratorTests
    {


        [Test]
        public void Should_Write_Each_Result()
        {
            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));
            results.Add(new WebAssetResolverResult("", false, null));

            generator.Generate(results);

            writer.Verify(w => w.Write(It.IsAny<WebAssetMergerResult>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Merge_Each_Result()
        {
            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));
            results.Add(new WebAssetResolverResult("", false, null));

            generator.Generate(results);

            merger.Verify(w => w.Merge(It.IsAny<WebAssetResolverResult>()), Times.Exactly(2));
        }
    }
}
