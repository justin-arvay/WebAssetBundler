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
        public void Should_Combine_Into_Path()
        {
            var resolver = new PathResolver(WebAssetType.StyleSheet);
            var path = resolver.Resolve("path", "1.1", "test");

            Assert.AreEqual("path\\1.1\\test.css", path);
        }


        [Test]
        public void Should_Combine_Without_Version()
        {
            var resolver = new PathResolver(WebAssetType.StyleSheet);

            var path = resolver.Resolve("path", "", "test");
            Assert.AreEqual("path\\test.css", path);

            path = resolver.Resolve("path", null, "test");
            Assert.AreEqual("path\\test.css", path);
        }
    }
}
