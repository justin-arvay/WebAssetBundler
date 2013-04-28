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
                Assert.AreSame(directory, dir.Parent);
            }
        }

        [Test]
        public void Should_Get_Files()
        {
            var files = new List<IFile>(directory.GetFiles("*", System.IO.SearchOption.AllDirectories));

            Assert.AreEqual(4, files.Count);

            foreach (var file in files)
            {
                Assert.AreSame(directory, file.Directory);
            }
        }

        [Test]
        public void Should_Get_Path()
        {
        }

        [Test]
        public void Should_Exist()
        {
        }

        [Test]
        public void Should_Get_Attributes()
        {
        }

        [Test]
        public void Should_Get_Root_Directory()
        {           
            var directories = directory.GetDirectories();

            foreach (var dir in directories)
            {
                Assert.AreSame(dir.Parent, directory.GetRootDirectory());
            }

        }

        [Test]
        public void Should_Not_Get_Root_Directory()
        {           
            Assert.AreSame(directory, directory.GetRootDirectory());
        }

        [Test]
        public void Should_Get_Absolute_Path_From_Virtual()
        {
            var path = directory.GetAbsolutePath("~/AssetFileTest.css");

            Assert.AreEqual("../../Files/FileSystem\\AssetFileTest.css", path);
        }

        [Test]
        public void Should_Get_Absolute_Path_From_Relative()
        {
            var path = directory.GetAbsolutePath("AssetFileTest.css");

            Assert.AreEqual("../../Files/FileSystem\\AssetFileTest.css", path);
        }

        [Test]
        public void Should_Remove_File_Name_When_Getting_Directory()
        {
        }

        [Test]
        public void Should_Get_Directory_By_Virtual_Path()
        {
            //all virtuals should be based on the root, regardless of heirarchy

            var dir1 = directory.GetDirectory("~/Test/");
            var dir2 = dir1.GetDirectory("~/TestTwo/Inner");

            Assert.AreEqual("../../Files/FileSystem\\Test/", dir1.FullPath);
            Assert.AreEqual("../../Files/FileSystem\\TestTwo/Inner", dir2.FullPath);
        }

        [Test]
        public void Should_Get_Directory_By_Relative_Path()
        {
            //relative paths should get the directory as if it where inside the current directory

            var dir1 = directory.GetDirectory("Test/");
            var dir2 = dir1.GetDirectory("TestTwo/Inner");

            Assert.AreEqual("../../Files/FileSystem\\Test/", dir1.FullPath);
            Assert.AreEqual("../../Files/FileSystem\\Test/TestTwo/Inner", dir2.FullPath);
        }

        [Test]
        public void Should_Get_Directory_By_Absolute_Path()
        {
            //relative paths should get the directory as if it where inside the current directory

            var dir1 = directory.GetDirectory("../../Files/FileSystem/Test/");
            var dir2 = dir1.GetDirectory("../../Files/FileSystem/TestTwo/Inner");

            Assert.AreEqual("../../Files/FileSystem/Test/", dir1.FullPath);
            Assert.AreEqual("../../Files/FileSystem/TestTwo/Inner", dir2.FullPath);
        }

        [Test]
        public void Should_Get_Current_Directory_If_Path_Emtpy()
        {
            var dir1 = directory.GetDirectory("");

            Assert.AreSame(dir1, directory);
        }

        [Test]
        public void Should_Get_File_By_Virtual_Path()
        {
            var file = directory.GetFile("~/Dir/Test.css");

            Assert.AreEqual("../../Files/FileSystem\\Dir\\Test.css", file.Path);
        }

        [Test]
        public void Should_Get_File_By_Relative_Path()
        {
            var file = directory.GetFile("Dir/Test.css");

            Assert.AreEqual("../../Files/FileSystem\\Dir\\Test.css", file.Path);
        }

        [Test]
        public void Should_Get_File_By_Absolute_Path()
        {
            var file = directory.GetFile("../../Files/FileSystem/Dir/Test.css");

            Assert.AreEqual("../../Files/FileSystem\\Dir\\Test.css", file.Path);
        }
    }
}
