

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedWebAssetGroupFactoryTests
    {
        private ISharedWebAssetGroupFactory factory;


        [SetUp]
        public void Setup()
        {
            factory = new SharedWebAssetGroupFactory();
        }


        [Test]
        public void Should_Be_Shared_Group()
        {
            Assert.IsTrue(factory.Create(new GroupConfigurationElementCollection()).IsShared);
            
        }

        [Test]
        public void Should_Use_Generated_Path()
        {
            Assert.AreEqual(DefaultSettings.GeneratedFilesPath, factory.Create(new GroupConfigurationElementCollection()).GeneratedPath);
        }

        [Test]
        public void Should_Map_Name()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Name = "Foo";

            Assert.AreEqual("Foo", factory.Create(collection).Name);
        }

        [Test]
        public void Should_Map_Compress()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Compress = true;

            Assert.IsTrue(factory.Create(collection).Compress);
        }

        [Test]
        public void Should_Map_Version()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Version = "1.1";

            Assert.AreEqual("1.1", factory.Create(collection).Version);
        }

        [Test]
        public void Should_Map_Combine()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Combine = true;

            Assert.IsTrue(factory.Create(collection).Combine);
        }
    }
}
