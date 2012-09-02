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
        private Mock<IPathResolver> resolver;
        private Mock<HttpServerUtilityBase> server;

        [SetUp]
        public void Setup()
        {
            reader = new Mock<IWebAssetReader>();
            filter = new Mock<IContentFilter>();
            resolver = new Mock<IPathResolver>();
            server = new Mock<HttpServerUtilityBase>();
            compressor = new Mock<IStyleSheetCompressor>();
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(r => r.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            Assert.AreEqual(content + content, merger.Merge(results)[0].Content);
        }

        [Test]
        public void Should_Return_Merger_Result_Path_As_Result_Path()
        {
            var path = "path/test.css";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var results = new List<ResolverResult>();            

            results.Add(new ResolverResult(new List<IWebAsset>(), "Test")
            {
                Version = "1.1"
            });

            resolver.Setup(r => r.Resolve(
                It.Is<string>(s => s.Equals(DefaultSettings.GeneratedFilesPath)),
                It.Is<string>(s => s.Equals("1.1")),
                It.Is<string>(s => s.Equals("Test")))).Returns(path);

            Assert.AreEqual(path, merger.Merge(results)[0].Path);
        }

        [Test]
        public void Should_Filter_Each_WebAsset()
        {
            var content = "1";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            merger.Merge(results);

            //verify that we called the filter twice
            filter.Verify(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Map_Source_And_Output_Paths()
        {
            var outputPath = "~/Generated/File.css";
            var sourcePath = "~/Content/File.css";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(sourcePath)); resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(outputPath);

            merger.Merge(results);
           
            //Verify it calles mappath once with the outputPath as a param
            server.Verify(r => r.MapPath(It.Is<string>(s => s.Equals(outputPath))), Times.Exactly(1));

            //Verify it calls mappaath once with the sourcePath as a param
            server.Verify(r => r.MapPath(It.Is<string>(s => s.Equals(sourcePath))), Times.Exactly(1));
        }

        [Test]
        public void Should_Compress_Content()
        {
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test") { Compress = true });

            webAssets.Add(new WebAsset(""));

            merger.Merge(results);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Should_Not_Compress_Content()
        {
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));

            merger.Merge(results);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void Should_Pass_Correct_Values_To_Filter()
        {
            var content = "1";
            var path ="~/Test/file.css";
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            resolver.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(path);

            server.Setup(r => r.MapPath(path)).Returns(path);

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            merger.Merge(results);
        }

        [Test]
        public void Should_Set_Host()
        {
            var merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, resolver.Object, server.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();
            var result = new ResolverResult(webAssets, "Test");


            result.Host = "192.168.1.1";
            results.Add(result);
            webAssets.Add(new WebAsset(""));

            var mergedResults = merger.Merge(results);

            Assert.AreSame("192.168.1.1", mergedResults[0].Host);
        }
    }
}
