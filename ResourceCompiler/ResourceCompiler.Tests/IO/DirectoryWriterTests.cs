namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using System.IO;

    [TestFixture]
    public class DirectoryWriterTests
    {
        private string basePath = "Files/Generated/";

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(basePath);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(basePath, true);
        }

        [Test]
        public void Should_Write_Full_Directory_Structure()
        {
            var path = Path.Combine(basePath, "fake-dir/file.js");
            var writer = new DirectoryWriter();

            writer.Write(path);

            Assert.True(Directory.Exists(Path.GetDirectoryName(path)));
        }
    }
}
