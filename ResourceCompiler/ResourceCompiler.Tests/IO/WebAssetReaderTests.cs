namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using System.Web.Hosting;
    using System.Web;
    using Moq;

    [TestFixture]
    public class WebAssetReaderTests
    {
        [Test]
        public void Should_Read_Content_From_Web_Asset()
        {
            var server = new Mock<HttpServerUtilityBase>();   
            var reader = new WebAssetReader(server.Object);
            var webAsset = new WebAsset("Files/read.txt");

            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            Assert.AreEqual("Line 1", reader.Read(webAsset));
        }
    }
}
