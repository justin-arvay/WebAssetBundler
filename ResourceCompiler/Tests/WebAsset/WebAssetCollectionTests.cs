namespace Tests.WebAsset
{
    using System;
    using NUnit.Framework;
    using ResourceCompiler.WebAsset;

    [TestFixture]
    public class WebAssetCollectionTests
    {
        [Test]
        public void Should_Find_Group_By_Name()
        {
            var collection = new WebAssetGroupCollection();
            collection.Add(new WebAssetGroup("test", false));

            Assert.NotNull(collection.FindGroupByName("test"));
        }

        [Test]
        public void Should_Find_Group_By_Name_Regardless_Of_Case()
        {
            var collection = new WebAssetGroupCollection();
            collection.Add(new WebAssetGroup("Test", false));

            Assert.NotNull(collection.FindGroupByName("tEST"));
        }
       
    }
}
