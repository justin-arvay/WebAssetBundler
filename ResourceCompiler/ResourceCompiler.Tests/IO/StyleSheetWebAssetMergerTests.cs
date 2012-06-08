// WebAssetBundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Web;

    [TestFixture]
    public class StyleSheetWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;
        private Mock<IContentFilter> filter;
        private Mock<IStyleSheetCompressor> compressor;
        private Mock<HttpServerUtilityBase> server;

        [SetUp]
        public void Setup()
        {
            reader = new Mock<IWebAssetReader>();
            filter = new Mock<IContentFilter>();
            server = new Mock<HttpServerUtilityBase>();
            compressor = new Mock<IStyleSheetCompressor>();
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new ResolverResult("", false, webAssets);

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
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object);
            var resolverResult = new ResolverResult(path, false, new List<IWebAsset>());

            Assert.AreEqual(path, merger.Merge(resolverResult).Path);
        }

        [Test]
        public void Should_Filter_Each_WebAsset()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new ResolverResult("", false, webAssets);

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
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new ResolverResult(outputPath, false, webAssets);

            webAssets.Add(new WebAsset(sourcePath));            

            merger.Merge(resolverResult);

            //Verify it calles mappath once with the outputPath as a param
            server.Verify(r => r.MapPath(It.Is<string>(s => s.Equals(outputPath))), Times.Exactly(1));

            //Verify it calls mappaath once with the sourcePath as a param
            server.Verify(r => r.MapPath(It.Is<string>(s => s.Equals(sourcePath))), Times.Exactly(1));
        }

        [Test]
        public void Should_Compress_Content()
        {
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new ResolverResult("", true, webAssets);

            webAssets.Add(new WebAsset(""));

            merger.Merge(resolverResult);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Should_Not_Compress_Content()
        {
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var resolverResult = new ResolverResult("", false, webAssets);

            webAssets.Add(new WebAsset(""));

            merger.Merge(resolverResult);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Never());
        }
    }
}
