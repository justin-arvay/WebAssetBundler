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
            var readValue = "1";
            var reader = new Mock<IWebAssetReader>();
            var merger = new VersionedWebAssetGroupMerger(reader.Object);

            //setup the reader to return the readValue for any web asset
            reader.Setup(m => m.Read(It.IsAny<IWebAsset>()))
                .Returns(readValue);

            var webAssets = new List<IWebAsset>();
            webAssets.Add(new WebAsset("not/real.txt"));
            webAssets.Add(new WebAsset("not/real.txt"));

            var returnedContents = (IList<string>)merger.Merge(webAssets);

            //should have the smae amount as the number of assets
            Assert.AreEqual(returnedContents.Count, 2);

            foreach (var content in returnedContents)
            {
                //each piece of content should match what the reader gave us
                Assert.AreEqual(content, readValue);
            }
        }
    }
}
