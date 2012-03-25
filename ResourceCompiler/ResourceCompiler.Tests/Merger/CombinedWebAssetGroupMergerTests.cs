namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;

    [TestFixture]
    public class CombinedWebAssetGroupMergerTests
    {
        private Mock<IWebAssetReader> reader;

        public CombinedWebAssetGroupMergerTests()
        {
            reader = new Mock<IWebAssetReader>();
        }

        [Test]
        public void Should_Return_Merger_Result_With_Group_Name_As_Merger_Name()
        {
            var group = new WebAssetGroup("test", false);
            var merger = new CombinedWebAssetGroupMerger(group, reader.Object);

            var result =  merger.Merge()[0];

            Assert.AreEqual(group.Name, result.Name);
        }

        [Test]
        public void Should_Return_Merger_Result_With_Group_Version_As_Merger_Version()
        {
            var group = new WebAssetGroup("test", false) { Version = "1.2" };
            var merger = new CombinedWebAssetGroupMerger(group, reader.Object);

            var result = merger.Merge()[0];

            Assert.AreEqual(group.Version, result.Version);
        }

        [Test]
        public void Should_Return_Merger_Result_With_All_Asset_Content_Merged()
        {
            var readValue = "1";
            var group = new WebAssetGroup("test", false);
            var merger = new CombinedWebAssetGroupMerger(group, reader.Object);

            //set up 2 so we call the reader twice
            group.Assets.Add(new WebAsset("fake.js"));
            group.Assets.Add(new WebAsset("fake2.js"));

            //setup the reader to return the readValue for any web asset
            reader.Setup(m => m.Read(It.IsAny<IWebAsset>()))
                .Returns(readValue);

            var result = merger.Merge()[0];

            Assert.AreEqual(readValue + readValue, result.Content);
        }
    }
}
