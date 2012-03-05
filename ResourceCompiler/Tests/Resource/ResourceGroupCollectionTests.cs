namespace Tests.Resource
{
    using System;
    using NUnit.Framework;
    using ResourceCompiler.Resource;

    [TestFixture]
    public class ResourceGroupCollectionTests
    {
        [Test]
        public void Should_Find_Group_By_Name()
        {
            var collection = new ResourceGroupCollection();
            collection.Add(new ResourceGroup("test", false));

            Assert.NotNull(collection.FindGroupByName("test"));
        }

        [Test]
        public void Should_Find_Group_By_Name_Regardless_Of_Case()
        {
            var collection = new ResourceGroupCollection();
            collection.Add(new ResourceGroup("Test", false));

            Assert.NotNull(collection.FindGroupByName("tEST"));
        }
       
    }
}
