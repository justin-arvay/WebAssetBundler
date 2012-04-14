
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Web;

    [TestFixture]
    public class ScriptWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;
        private Mock<IScriptCompressor> compressor;
        
        [SetUp]
        public void Setup()
        {
            reader = new Mock<IWebAssetReader>();
            compressor = new Mock<IScriptCompressor>();

            compressor.Setup(c => c.Compress(It.IsAny<string>()))
                .Returns((string content) => content);
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets_With_Delimeter()
        {
            var content = "function(){}";
            var merger = new ScriptWebAssetMerger(reader.Object, compressor.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", false, webAssets);

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
            var merger = new ScriptWebAssetMerger(reader.Object, compressor.Object);
            var resolverResult = new WebAssetResolverResult(path, false, new List<IWebAsset>());

            Assert.AreEqual(path, merger.Merge(resolverResult).Path);
        }

        [Test]
        public void Should_Compress_Merged_Content()
        {            
            var merger = new ScriptWebAssetMerger(reader.Object, compressor.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", true, webAssets);

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            merger.Merge(resolverResult);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Once());                       
        }

        [Test]
        public void Should_Not_Compress_Merged_Content()
        {
            var merger = new ScriptWebAssetMerger(reader.Object, compressor.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new WebAssetResolverResult("", false, webAssets);

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            merger.Merge(resolverResult);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Never());                       
        }
    }
}