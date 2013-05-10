// Web Asset Bundler - Bundles web assets so you dont have to.
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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.IO;
    using System.Text;

    [TestFixture]
    public class FileAssetTest
    {
        private Mock<IFile> file;
        private FileAsset asset;

        [SetUp]
        public void Setup()
        {
            file = new Mock<IFile>();
            asset = new FileAsset(file.Object);
        }

        [Test]
        public void Should_Get_Source()
        {
            file.Setup(f => f.Path).Returns("D:/Source/File.cs");

            Assert.AreEqual("D:/Source/File.cs", asset.Source);
        }

        [Test]
        public void Should_Get_Content()
        {
            Stream stream = new MemoryStream(Encoding.ASCII.GetBytes("test"));
            file.Setup(f => f.Open(FileMode.Open, FileAccess.Read, FileShare.Read)).Returns(stream);

            Assert.AreEqual("test", asset.OpenStream().ReadToEnd());
        }
    }
}
