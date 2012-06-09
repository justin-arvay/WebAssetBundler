
namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class SharedGroupManagerFactoryTests
    {
        private Mock<ISharedGroupConfigurationLoader> mapper;
        
        [SetUp]
        public void Setup()
        {
            mapper = new Mock<ISharedGroupConfigurationLoader>();
        }

        [Test]
        public void Should_Always_Return_Same_Instance()
        {
            var factory = new SharedGroupManagerFactory(mapper.Object);

            Assert.AreSame(factory.Create(), factory.Create());
        }

        [Test]
        public void Should_Map_StyleSheets()
        {
            var factory = new SharedGroupManagerFactory(mapper.Object);
            factory.Create();

            mapper.Verify(m => m.LoadStyleSheets(It.IsAny<IList<WebAssetGroup>>()), Times.Once());
        }

        [Test]
        public void Should_Map_Scipts()
        {
            var factory = new SharedGroupManagerFactory(mapper.Object); 
            factory.Create();

            mapper.Verify(m => m.LoadScripts(It.IsAny<IList<WebAssetGroup>>()), Times.Once());
        }
    }
}
