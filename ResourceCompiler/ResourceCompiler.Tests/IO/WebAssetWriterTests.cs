// ResourceCompiler - Compiles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using Moq;
    using System.IO;
    using System.Web;

    [TestFixture]
    public class WebAssetWriterTests
    {        
        private Mock<IDirectoryWriter> dirWriter;
        private Mock<HttpServerUtilityBase> server;

        public WebAssetWriterTests()
        {
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
            
            var writer = new WebAssetWriter(dirWriter.Object, server.Object);
            var result = new WebAssetMergerResult(filePath, "content");

            //map the mapPath call return whatever was passed
            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            writer.Write(result);

            Assert.True(File.Exists(filePath));
        }

        [Test]
        public void Should_Write_Content_To_File()
        {
            var filePath = "Files/Generated/file-test.css";
            var content = "This content should be written.";

            var writer = new WebAssetWriter(dirWriter.Object, server.Object);
            var result = new WebAssetMergerResult(filePath, content);

            //make the mapPath call return whatever was passed
            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            writer.Write(result);

            using (var reader = new StreamReader(filePath))
            {
                Assert.AreEqual(content, reader.ReadToEnd());
            }
        }

        [Test]
        public void Should_Pass_Full_File_Path_To_Directory_Writer()
        {
            var filePath = "Files/Generated/does-not-exist/file.css";
            var content = "This content should be written.";

            var writer = new WebAssetWriter(dirWriter.Object, server.Object);
            var result = new WebAssetMergerResult(filePath, content);

            //make the mapPath call return whatever was passed
            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            //the streamwriter will throw an exception because the directory doesnt actually exist so just catch it to ignore since we dont want to actually creating it
            Assert.Throws<DirectoryNotFoundException>(() => writer.Write(result));

            //verify the filepath is passed to the directory writer write method
            dirWriter.Verify(d => d.Write(It.Is<string>(s => s.Equals(filePath))), Times.Exactly(1));
        }
    }
}
