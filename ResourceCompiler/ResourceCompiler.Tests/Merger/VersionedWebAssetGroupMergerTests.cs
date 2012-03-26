namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class VersionedWebAssetGroupMergerTests
    {
        private Mock<IWebAssetReader> reader;

        private readonly string readValue = "1";

        public VersionedWebAssetGroupMergerTests()
        {
            reader = new Mock<IWebAssetReader>();

            //setup the reader to return the readValue for any web asset
            reader.Setup(m => m.Read(It.IsAny<IWebAsset>()))
                .Returns(readValue);
        }

        [Test]
        public void Should_Return_Content_From_Each_Asset()
        {
            var group = new WebAssetGroup("test", false);
            var merger = new VersionedWebAssetGroupMerger(group, reader.Object);

            group.Assets.Add(new WebAsset("not/real1.txt"));
            group.Assets.Add(new WebAsset("not/real2.txt"));

            var results = merger.Merge();

            //should have the smae amount as the number of assets
            Assert.AreEqual(results.Count, 2);

            foreach (var result in results)
            {
                //each piece of content should match what the reader gave us
                Assert.AreEqual(result.Content, readValue);
            }
        }

        [Test]
        public void Should_Return_Merger_Result_With_Group_Name_As_Merger_Name()
        {
            string name = "test";
            var group = new WebAssetGroup(name, false);
            var merger = new VersionedWebAssetGroupMerger(group, reader.Object);

            group.Assets.Add(new WebAsset("not/real1.txt"));

            var result = merger.Merge()[0];

            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public void Should_Return_Merger_Result_With_Asset_Version_As_Merger_Version()
        {
            string version = "1.1";
            var group = new WebAssetGroup("test", false) { Version = version };
            var merger = new VersionedWebAssetGroupMerger(group, reader.Object);

            group.Assets.Add(new WebAsset("not/real1.txt"));

            var result = merger.Merge()[0];

            Assert.AreEqual(version, result.Version);
        }
    }
}
