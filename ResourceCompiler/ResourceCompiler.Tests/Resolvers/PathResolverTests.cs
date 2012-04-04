namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;
    using System.Web;

    [TestFixture]
    public class PathResolverTests
    {
        [Test]
        public void Should_Combine_Version_Into_Path()
        {
            var resolver = new PathResolver();
            var path = resolver.Resolve("path", "1.1", "test", "css");

            Assert.AreEqual("path\\1.1\\test.css", path);
        }
    }
}
