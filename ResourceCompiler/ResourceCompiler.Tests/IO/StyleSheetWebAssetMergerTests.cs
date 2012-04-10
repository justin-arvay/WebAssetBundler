
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Web;

    [TestFixture]
    public class StyleSheetWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;
        private Mock<IWebAssetContentFilter> filter;
        private Mock<HttpServerUtilityBase> server;

        public StyleSheetWebAssetMergerTests()
        {
            reader = new Mock<IWebAssetReader>();
            filter = new Mock<IWebAssetContentFilter>();
            server = new Mock<HttpServerUtilityBase>();
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", webAssets);

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(r => r.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            Assert.AreEqual(content + content, merger.Merge(resolverResult).Content);
        }

        [Test]
        public void Should_Return_Merger_Result_Path_As_Result_Path()
        {
            var path = "path/test.css";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, server.Object);
            var resolverResult = new WebAssetResolverResult(path, new List<IWebAsset>());

            Assert.AreEqual(path, merger.Merge(resolverResult).Path);
        }

        [Test]
        public void Should_Filter_Each_WebAsset()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", webAssets);

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            merger.Merge(resolverResult);

            //verify that we called the filter twice
            filter.Verify(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Map_Source_And_Output_Paths()
        {
            var outputPath = "~/Generated/File.css";
            var sourcePath = "~/Content/File.css";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult(outputPath, webAssets);

            webAssets.Add(new WebAsset(sourcePath));            

            merger.Merge(resolverResult);

            //Verify it calles mappath once with the outputPath as a param
            server.Verify(r => r.MapPath(It.Is<string>(s => s.Equals(outputPath))), Times.Exactly(1));

            //Verify it calls mappaath once with the sourcePath as a param
            server.Verify(r => r.MapPath(It.Is<string>(s => s.Equals(sourcePath))), Times.Exactly(1));
        }
    }
}
