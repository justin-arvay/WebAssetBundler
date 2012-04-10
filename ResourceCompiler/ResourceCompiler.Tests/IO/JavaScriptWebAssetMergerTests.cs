
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Web;

    [TestFixture]
    public class JavaScriptWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;

        public JavaScriptWebAssetMergerTests()
        {
            reader = new Mock<IWebAssetReader>();
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets_With_Delimeter()
        {
            var content = "function(){}";
            var merger = new JavaScriptWebAssetMerger(reader.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", webAssets);

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            Assert.AreEqual(content + ";" + content + ";", merger.Merge(resolverResult).Content);
        }

        [Test]
        public void Should_Return_Merger_Result_Path_As_Result_Path()
        {
            var path = "path/test.js";
            var merger = new JavaScriptWebAssetMerger(reader.Object);
            var resolverResult = new WebAssetResolverResult(path, new List<IWebAsset>());

            Assert.AreEqual(path, merger.Merge(resolverResult).Path);
        }
    }
}
