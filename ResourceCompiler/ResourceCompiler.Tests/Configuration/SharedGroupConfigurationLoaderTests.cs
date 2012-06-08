﻿

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedGroupConfigurationLoaderTests
    {
        private Mock<ISharedWebAssetFactory> assetFactory;
        private Mock<ISharedWebAssetGroupFactory> groupFactory;
        private Mock<IConfigurationSectionFactory> sectionFactory;        

        [SetUp]
        public void Setup()
        {
            assetFactory = new Mock<ISharedWebAssetFactory>();
            groupFactory = new Mock<ISharedWebAssetGroupFactory>();
            sectionFactory = new Mock<IConfigurationSectionFactory>();

            sectionFactory.Setup(s => s.Create()).Returns(ConfigurationTestHelper.CreateSection());
            groupFactory.Setup(g => g.Create(It.IsAny<GroupConfigurationElementCollection>()))
                .Returns(new WebAssetGroup("", true, ""));

            assetFactory.Setup(a => a.Create(It.IsAny<AssetConfigurationElement>()))
                .Returns(new WebAsset(""));
        }

        [Test]
        public void Should_Add_Style_Sheet_Group()
        {
            var manager = new SharedGroupManager();
            var loader = new SharedGroupConfigurationLoader(sectionFactory.Object, groupFactory.Object, assetFactory.Object);
            loader.LoadStyleSheets(manager.StyleSheets);

            groupFactory.Verify(a => a.Create(It.IsAny<GroupConfigurationElementCollection>()), Times.Once());                            
   
        }

        [Test]
        public void Should_Add_Script_Group()
        {
            var manager = new SharedGroupManager();
            var loader = new SharedGroupConfigurationLoader(sectionFactory.Object, groupFactory.Object, assetFactory.Object);
            loader.LoadStyleSheets(manager.Scripts);

            groupFactory.Verify(a => a.Create(It.IsAny<GroupConfigurationElementCollection>()), Times.Once());                            
        }

        [Test]
        public void Should_Add_Assets_To_Style_Sheet_Groups()
        {
            var manager = new SharedGroupManager();
            var loader = new SharedGroupConfigurationLoader(sectionFactory.Object, groupFactory.Object, assetFactory.Object);
            loader.LoadStyleSheets(manager.StyleSheets);

            assetFactory.Verify(a => a.Create(It.IsAny<AssetConfigurationElement>()), Times.Once());                            
        }

        [Test]
        public void Should_Add_Assets_To_Script_Groups()
        {
            var manager = new SharedGroupManager();
            var loader = new SharedGroupConfigurationLoader(sectionFactory.Object, groupFactory.Object, assetFactory.Object);
            loader.LoadStyleSheets(manager.Scripts);

            assetFactory.Verify(a => a.Create(It.IsAny<AssetConfigurationElement>()), Times.Once());      
        }

        [Test]
        public void Should_Not_Load_Scripts()
        {
            sectionFactory.Setup(s => s.Create())
                .Returns(() => null);

            var manager = new SharedGroupManager();
            var loader = new SharedGroupConfigurationLoader(sectionFactory.Object, groupFactory.Object, assetFactory.Object);
            loader.LoadScripts(manager.Scripts);

            groupFactory.Verify(a => a.Create(It.IsAny<GroupConfigurationElementCollection>()), Times.Never());                            
            assetFactory.Verify(a => a.Create(It.IsAny<AssetConfigurationElement>()), Times.Never());                             
                           
        }

        [Test]
        public void Should_Not_Load_Style_Sheets()
        {
            sectionFactory.Setup(s => s.Create())
                .Returns(() => null);

            var manager = new SharedGroupManager();
            var loader = new SharedGroupConfigurationLoader(sectionFactory.Object, groupFactory.Object, assetFactory.Object);
            loader.LoadStyleSheets(manager.StyleSheets);

            groupFactory.Verify(a => a.Create(It.IsAny<GroupConfigurationElementCollection>()), Times.Never());
            assetFactory.Verify(a => a.Create(It.IsAny<AssetConfigurationElement>()), Times.Never());       
        }
    }
}