

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedGroupManagerTests
    {
        [Test]
        public void Should_Have_Empty_Scripts()
        {
            var manager = new SharedGroupManager();

            Assert.AreEqual(0, manager.Scripts.Count);
        }

        [Test]
        public void Should_Have_Empty_StyleSheets()
        {
            var manager = new SharedGroupManager();

            Assert.AreEqual(0, manager.StyleSheets.Count);
        }        
    }
}
