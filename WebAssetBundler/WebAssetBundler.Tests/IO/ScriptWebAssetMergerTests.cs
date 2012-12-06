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
    public class ScriptWebAssetMergerTests
    {
        private Mock<IWebAssetReader> reader;
        private Mock<IScriptCompressor> compressor;
        private Mock<IMergedBundleCache<ScriptBundle>> cache;
        private ScriptWebAssetMerger merger;
        private BuilderContext context;
        
        [SetUp]
        public void Setup()
        {
            reader = new Mock<IWebAssetReader>();
            compressor = new Mock<IScriptCompressor>();
            cache = new Mock<IMergedBundleCache<ScriptBundle>>();
            context = new BuilderContext();

            merger = new ScriptWebAssetMerger(reader.Object, compressor.Object, cache.Object);

            compressor.Setup(c => c.Compress(It.IsAny<string>()))
                .Returns((string content) => content);
        }

        [Test]
        public void Should_Merge_Content_From_Result_Assets_With_Delimeter()
        {
            var content = "function(){}";            
            var assets = new List<AssetBase>();
            var results = new List<ResolvedBundle>();

            results.Add (new ResolvedBundle(assets, "Test")
                {
                    Host = "http://www.test.com"
                });

            assets.Add(new AssetBase(""));
            assets.Add(new AssetBase(""));

            //set up the reader to always return content
            reader.Setup(r => r.Read(It.IsAny<IWebAsset>()))
                .Returns(content);

            var result = merger.Merge(results, context)[0];

            Assert.AreEqual(content + ";" + content + ";", result.Content);
            Assert.AreEqual("Test", result.Name);
            Assert.AreEqual("http://www.test.com", result.Host);
        }

        [Test]
        public void Should_Compress_Merged_Content()
        {
            var webAssets = new List<AssetBase>();
            var results = new List<ResolvedBundle>();

            results.Add(new ResolvedBundle(webAssets, "Test") { Compress = true });

            webAssets.Add(new AssetBase(""));
            webAssets.Add(new AssetBase(""));

            merger.Merge(results, context);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Once());                       
        }

        [Test]
        public void Should_Not_Compress_Merged_Content()
        {
            var webAssets = new List<AssetBase>();
            var results = new List<ResolvedBundle>();

            results.Add(new ResolvedBundle(webAssets, "Test"));

            webAssets.Add(new AssetBase(""));
            webAssets.Add(new AssetBase(""));

            merger.Merge(results, context);

            compressor.Verify(c => c.Compress(It.IsAny<string>()), Times.Never());                       
        }

        [Test]
        public void Should_Cache_Result()
        {
            var results = new List<ResolvedBundle>();
            results.Add(new ResolvedBundle(new List<AssetBase>(), "Test"));

            merger.Merge(results, context);

            cache.Verify(c => c.Add(It.IsAny<MergedBundle>()), Times.Once());
        }

        [Test]
        public void Should_Not_Add_To_Cache()
        {
            var webAssets = new List<AssetBase>();
            var results = new List<ResolvedBundle>();
            var result = new ResolvedBundle(webAssets, "Test");

            results.Add(result);
            webAssets.Add(new AssetBase(""));

            cache.Setup(c => c.Get(It.IsAny<string>())).Returns(new MergedBundle("", "", WebAssetType.None));

            var mergedResults = merger.Merge(results, context);

            cache.Verify(c => c.Add(It.IsAny<MergedBundle>()), Times.Never());
        }

        [Test]
        public void Should_Always_Cache_In_Debug_Mode()
        {
            context.DebugMode = true;

            var webAssets = new List<AssetBase>();
            var results = new List<ResolvedBundle>();
            var result = new ResolvedBundle(webAssets, "Test");

            results.Add(result);
            webAssets.Add(new AssetBase(""));

            cache.Setup(c => c.Get(It.IsAny<string>())).Returns(new MergedBundle("", "", WebAssetType.None));

            var mergedResults = merger.Merge(results, context);

            cache.Verify(c => c.Add(It.IsAny<MergedBundle>()), Times.Once());
        }
    }
}