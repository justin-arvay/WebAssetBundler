namespace Tests.Resource
{
    using System;
    using NUnit.Framework;
    using ResourceCompiler.Resource;

    [TestFixture]
    public class ResourceGroupCollectionBuilderTests
    {
        [Test]
        public void Should_Be_Able_To_Add_Group()
        {
            var collection = new ResourceGroupCollection();
            var builder = new ResourceGroupCollectionBuilder(ResourceType.None, collection);

            builder.AddGroup("test", g => g.ToString());

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void Should_Throw_Exception_When_Adding_Group_That_Already_Exists()
        {
            var collection = new ResourceGroupCollection();
            var builder = new ResourceGroupCollectionBuilder(ResourceType.None, collection);

            builder.AddGroup("test", g => g.ToString());

            Assert.Throws<ArgumentException>(() => builder.AddGroup("test", g => g.ToString()));           
        }

        [Test]
        public void Should_Have_Nothing_In_Collection_By_Default()
        {
            var collection = new ResourceGroupCollection();
            var builder = new ResourceGroupCollectionBuilder(ResourceType.None, collection);

            Assert.AreEqual(0, collection.Count);
        }
    }
}
