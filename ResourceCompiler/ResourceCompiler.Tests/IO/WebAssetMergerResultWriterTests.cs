namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;
    using System.IO;

    [TestFixture]
    public class WebAssetMergerResultWriterTests
    {
        private Mock<IPathResolver> resolver;

        public WebAssetMergerResultWriterTests()
        {
            resolver = new Mock<IPathResolver>();
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
            var writer = new WebAssetMergerResultWriter("css", resolver.Object);
            var result = new WebAssetMergerResult("name", "1.1", "content");

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

            
            var writer = new WebAssetMergerResultWriter("css", resolver.Object);
            var result = new WebAssetMergerResult("name", "1.1", content);

            //have the resolver return an explicit path
            resolver.Setup(m => m.Resolve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(filePath);

            writer.Write(filePath, result);

            using (var reader = new StreamReader(filePath))
            {
                Assert.AreEqual(content, reader.ReadToEnd());
            }
        }
    }
}
