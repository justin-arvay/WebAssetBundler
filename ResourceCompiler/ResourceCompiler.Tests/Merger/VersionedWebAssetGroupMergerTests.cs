namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class VersionedWebAssetGroupMergerTests
    {
        [Test]
        public void Should_Return_Content_From_Each_Asset()
        {
            var group = new WebAssetGroup("test", false);
            var readValue = "1";
            var reader = new Mock<IWebAssetReader>();
            var merger = new VersionedWebAssetGroupMerger(group, reader.Object);

            //setup the reader to return the readValue for any web asset
            reader.Setup(m => m.Read(It.IsAny<IWebAsset>()))
                .Returns(readValue);

            group.Assets.Add(new WebAsset("not/real1.txt"));
            group.Assets.Add(new WebAsset("not/real2.txt"));

            var results = (IList<WebAssetMergerResult>)merger.Merge();

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
            Assert.Inconclusive();
        }

        [Test]
        public void Should_Return_Merger_Result_With_Asset_Version_As_Merger_Version()
        {
            Assert.Inconclusive();
        }
    }
}
