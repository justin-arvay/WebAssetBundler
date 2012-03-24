namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;

    [TestFixture]
    public class WebAssetGroupCollectionBuilderTests
    {
        [Test]
        public void Should_Be_Able_To_Add_Group()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection);

            builder.AddGroup("test", g => g.ToString());

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void Should_Throw_Exception_When_Adding_Group_That_Already_Exists()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection);

            builder.AddGroup("test", g => g.ToString());

            Assert.Throws<ArgumentException>(() => builder.AddGroup("test", g => g.ToString()));           
        }

        [Test]
        public void Should_Have_Nothing_In_Collection_By_Default()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection);

            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Adding_File_Should_Add_New_Group_With_Resource()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection);

            builder.Add("~/Files/test.css");
            builder.Add("~/Files/test.css");

            //should be 2 items in the collection, and 1 item in each collections group
            Assert.AreEqual(2, collection.Count);
            foreach (var group in collection) 
            {
                Assert.AreEqual(1, group.Assets.Count);
            }
        }

        [Test]
        public void Adding_Group_Should_Return_Self_For_Chaining()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection);

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.AddGroup("test", g => g.ToString()));
        }

        [Test]
        public void Add_Should_Return_Self_For_Chaining()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection);

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.Add("~/Files/test.css"));
        }
    }
}
