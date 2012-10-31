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
        private StyleSheetWebAssetMerger merger;
        private Mock<IMergedResultCache> cache;
        private BuilderContext context;

        [SetUp]
        public void Setup()
        {
            reader = new Mock<IWebAssetReader>();
            filter = new Mock<IContentFilter>();
            server = new Mock<HttpServerUtilityBase>();
            compressor = new Mock<IStyleSheetCompressor>();
            cache = new Mock<IMergedResultCache>();
            context = new BuilderContext();

            merger = new StyleSheetWebAssetMerger(reader.Object, filter.Object, compressor.Object, server.Object, cache.Object);            
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets()
        {
            var content = "1";            
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test") { 
                Host = "http://www.test.com"
            });

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(r => r.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            var result = merger.Merge(results, context)[0];
            Assert.AreEqual(content + content, result.Content);
            Assert.AreEqual("Test", result.Name);
            Assert.AreEqual("http://www.test.com", result.Host);
        }

        [Test]
        public void Should_Filter_Each_WebAsset()
        {
            var content = "1";            
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

            merger.Merge(results, context);

            //verify that we called the filter twice
            filter.Verify(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Map_Source_And_Output_Paths()
        {
            var outputPath = "/wab.axd/css/a/a";
            var sourcePath = "~/Content/File.css";            
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(sourcePath));

            merger.Merge(results, context);
           
            //Verify it calles mappath once with the outputPath as a param
            server.Verify(r => r.MapPath(outputPath), Times.Exactly(1));

            //Verify it calls mappaath once with the sourcePath as a param
            server.Verify(r => r.MapPath(sourcePath), Times.Exactly(1));
        }

        [Test]
        public void Should_Compress_Content()
        {            
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test") { Compress = true });

            webAssets.Add(new WebAsset(""));

            merger.Merge(results, context);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Should_Not_Compress_Content()
        {            
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));

            merger.Merge(results, context);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void Should_Pass_Correct_Values_To_Filter()
        {
            var content = "1";
            var path ="~/Test/file.css";            
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            server.Setup(r => r.MapPath(path)).Returns(path);

            //sets up the filter to return whatever was passed to its content variable
            filter.Setup(f => f.Filter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => content);

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            merger.Merge(results, context);
        }

        [Test]
        public void Should_Cache_Result()
        {
            var results = new List<ResolverResult>();
            results.Add(new ResolverResult(new List<IWebAsset>(), "Test"));

            merger.Merge(results, context);

            cache.Verify(c => c.Add(It.IsAny<MergerResult>()), Times.Once());
        }

        [Test]
        public void Should_Not_Add_To_Cache()
        {
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();
            var result = new ResolverResult(webAssets, "Test");

            results.Add(result);
            webAssets.Add(new WebAsset(""));

            cache.Setup(c => c.Get(It.IsAny<string>())).Returns(new MergerResult("", "", WebAssetType.None));

            var mergedResults = merger.Merge(results, context);

            cache.Verify(c => c.Add(It.IsAny<MergerResult>()), Times.Never());
        }

        [Test]
        public void Should_Always_Cache_In_Debug_Mode()
        {
            context.DebugMode = true;

            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();
            var result = new ResolverResult(webAssets, "Test");

            results.Add(result);
            webAssets.Add(new WebAsset(""));

            cache.Setup(c => c.Get(It.IsAny<string>())).Returns(new MergerResult("", "", WebAssetType.None));

            var mergedResults = merger.Merge(results, context);

            cache.Verify(c => c.Add(It.IsAny<MergerResult>()), Times.Once());
        } 
    }
}
