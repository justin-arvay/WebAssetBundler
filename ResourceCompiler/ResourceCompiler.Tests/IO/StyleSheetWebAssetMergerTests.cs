
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class StyleSheetWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;

        public StyleSheetWebAssetMergerTests()
        {
            reader = new Mock<IWebAssetReader>();
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", webAssets);

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            Assert.AreEqual(content + content, merger.Merge(resolverResult).Content);
        }

        [Test]
        public void Should_Return_Merger_Result_Path_As_Result_Path()
        {
            var path = "path/test.css";
            var merger = new StyleSheetWebAssetMerger(reader.Object);
            var resolverResult = new WebAssetResolverResult(path, new List<IWebAsset>());

            Assert.AreEqual(path, merger.Merge(resolverResult).Path);
        }
    }
}
