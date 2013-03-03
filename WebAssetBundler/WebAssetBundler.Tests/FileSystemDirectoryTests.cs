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
    using System.Collections.Generic;

    [TestFixture]
    public class FileSystemDirectoryTests
    {
        private FileSystemDirectory directory;

        [SetUp]
        public void Setup()
        {
            directory = new FileSystemDirectory("../../Files/FileSystem");
        }

        [Test]
        public void Should_Get_Child_Directories()
        {
            var directories = new List<IDirectory>(directory.GetDirectories());

            Assert.AreEqual(2, directories.Count);

            foreach (var dir in directories)
            {
                Assert.AreSame(directory, ((FileSystemDirectory)dir).Parent);
            }
        }

        [Test]
        public void Should_Get_Files()
        {
            var files = new List<IFile>(directory.GetFiles("*", System.IO.SearchOption.AllDirectories));

            Assert.AreEqual(4, files.Count);
        }
    }
}
