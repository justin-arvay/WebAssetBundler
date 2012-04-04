namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;
    using System.IO;
    using System.Web;

    [TestFixture]
    public class WebAssetMergerResultWriterTests
    {
        private Mock<IPathResolver> resolver;
        private Mock<IDirectoryWriter> dirWriter;
        private Mock<HttpServerUtilityBase> server;

        public WebAssetMergerResultWriterTests()
        {
            resolver = new Mock<IPathResolver>();
            dirWriter = new Mock<IDirectoryWriter>();
            server = new Mock<HttpServerUtilityBase>();
        }

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory("Files/Generated/");
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete("Files/Generated", true);
        }

        [Test]
        public void Should_Write_File_At_Correct_Location()
        {
            var filePath = "Files/Generated/file-test.css";
            
            var writer = new WebAssetMergerResultWriter("css", resolver.Object, dirWriter.Object, server.Object);
            var result = new WebAssetMergerResult("name", "1.1", "content");

            //map the mapPath call return whatever was passed
            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            //have the resolver return an explicit path
            resolver.Setup(m => m.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(filePath);

            writer.Write(filePath, result);

            Assert.True(File.Exists(filePath));
        }

        [Test]
        public void Should_Write_Content_To_File()
        {
            var filePath = "Files/Generated/file-test.css";
            var content = "This content should be written.";

            var writer = new WebAssetMergerResultWriter("css", resolver.Object, dirWriter.Object, server.Object);
            var result = new WebAssetMergerResult("name", "1.1", content);

            //make the mapPath call return whatever was passed
            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            //have the resolver return an explicit path
            resolver.Setup(m => m.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(filePath);

            writer.Write(filePath, result);

            using (var reader = new StreamReader(filePath))
            {
                Assert.AreEqual(content, reader.ReadToEnd());
            }
        }

        [Test]
        public void Should_Create_Directory_If_Doesnt_Exist()
        {
            Assert.Inconclusive();
        }
    }
}
