﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
    public class ScriptWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;
        private Mock<IPathResolver> resolver;
        private Mock<IScriptCompressor> compressor;
        
        [SetUp]
        public void Setup()
        {
            reader = new Mock<IWebAssetReader>();
            compressor = new Mock<IScriptCompressor>();
            resolver = new Mock<IPathResolver>();

            compressor.Setup(c => c.Compress(It.IsAny<string>()))
                .Returns((string content) => content);
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets_With_Delimeter()
        {
            var content = "function(){}";
            var merger = new ScriptWebAssetMerger(reader.Object, resolver.Object, compressor.Object);
            var assets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add (new ResolverResult(assets, "Test"));

            assets.Add(new WebAsset(""));
            assets.Add(new WebAsset(""));

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            Assert.AreEqual(content + ";" + content + ";", merger.Merge(results)[0].Content);
        }

        [Test]
        public void Should_Return_Merger_Result_Path_Resolved()
        {
            var path = "Generated/js/test.js";
            var merger = new ScriptWebAssetMerger(reader.Object, resolver.Object, compressor.Object);
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
        public void Should_Compress_Merged_Content()
        {            
            var merger = new ScriptWebAssetMerger(reader.Object, resolver.Object, compressor.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test") { Compress = true });

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            merger.Merge(results);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Once());                       
        }

        [Test]
        public void Should_Not_Compress_Merged_Content()
        {
            var merger = new ScriptWebAssetMerger(reader.Object, resolver.Object, compressor.Object);
            var webAssets = new List<IWebAsset>();
            var results = new List<ResolverResult>();

            results.Add(new ResolverResult(webAssets, "Test"));

            webAssets.Add(new WebAsset(""));
            webAssets.Add(new WebAsset(""));

            merger.Merge(results);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Never());                       
        }
    }
}