

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedGroupManagerTests
    {
        private SharedGroupManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new SharedGroupManager();
        }

        [Test]
        public void Should_Have_Empty_Scripts()
        {
            Assert.AreEqual(0, manager.Scripts.Count);
        }

        [Test]
        public void Should_Have_Empty_StyleSheets()
        {
            Assert.AreEqual(0, manager.StyleSheets.Count);
        }
    }
}
