

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedGroupManagerLoaderTests
    {
        private Mock<IAssetConfigurationFactory> factory;
        private ISharedGroupManagerLoader loader;
        private SharedGroupManager manager;

        [SetUp]
        public void Setup()
        {
            factory = new Mock<IAssetConfigurationFactory>();
            loader = new SharedGroupManagerLoader(factory.Object);
            manager = new SharedGroupManager();

            factory.Setup(s => s.CreateSection()).Returns(ConfigurationTestHelper.CreateSection());

            factory.Setup(g => g.CreateGroup(It.IsAny<GroupConfigurationElementCollection>()))
                .Returns(() => new WebAssetGroup("", true));

            factory.Setup(a => a.CreateAsset(It.IsAny<AssetConfigurationElement>()))
                .Returns(() => new WebAsset(""));
        }

        [Test]
        public void Should_Add_Style_Sheet_Group()
        {
            loader.Load(manager);

            factory.Verify(a => a.CreateGroup(It.IsAny<GroupConfigurationElementCollection>()), Times.Exactly(2));
            Assert.AreEqual(1, manager.StyleSheets.Count);
        }

        [Test]
        public void Should_Add_Script_Group()
        {
            loader.Load(manager);

            factory.Verify(a => a.CreateGroup(It.IsAny<GroupConfigurationElementCollection>()), Times.Exactly(2));
            Assert.AreEqual(1, manager.Scripts.Count);
        }

        [Test]
        public void Should_Add_Assets_To_Style_Sheet_Groups()
        {         
            loader.Load(manager);

            factory.Verify(a => a.CreateAsset(It.IsAny<AssetConfigurationElement>()), Times.Exactly(2));
            Assert.AreEqual(1, manager.StyleSheets[0].Assets.Count);                
        }

        [Test]
        public void Should_Add_Assets_To_Script_Groups()
        {            
            loader.Load(manager);
            
            factory.Verify(a => a.CreateAsset(It.IsAny<AssetConfigurationElement>()), Times.Exactly(2));
            Assert.AreEqual(1, manager.Scripts[0].Assets.Count);
        }

        [Test]
        public void Should_Not_Load_Scripts()
        {
            factory.Setup(s => s.CreateSection())
                .Returns(() => null);
            
            loader.Load(manager);

            factory.Verify(a => a.CreateGroup(It.IsAny<GroupConfigurationElementCollection>()), Times.Never());                            
            factory.Verify(a => a.CreateAsset(It.IsAny<AssetConfigurationElement>()), Times.Never());                             
                           
        }

        [Test]
        public void Should_Not_Load_Style_Sheets()
        {
            factory.Setup(s => s.CreateSection())
                .Returns(() => null);

            loader.Load(manager);

            factory.Verify(a => a.CreateGroup(It.IsAny<GroupConfigurationElementCollection>()), Times.Never());
            factory.Verify(a => a.CreateAsset(It.IsAny<AssetConfigurationElement>()), Times.Never());       
        }
    }
}
