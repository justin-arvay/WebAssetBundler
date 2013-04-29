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
    using System.IO;
    using System;

    [TestFixture]
    public class FileSystemDirectoryTests
    {
        private FileSystemDirectory directory;
        private string root;

        [SetUp]
        public void Setup()
        {
            root = PathHelper.NormalizePath(AppDomain.CurrentDomain.BaseDirectory + "/../../");
            directory = new FileSystemDirectory(root);            
        }

        [Test]
        public void Should_Get_Child_Directories()
        {
            var parentDir = directory.GetDirectory("Files/FileSystem");
            var directories = new List<IDirectory>(parentDir.GetDirectories());

            Assert.AreEqual(2, directories.Count);

            foreach (var dir in directories)
            {
                Assert.AreSame(parentDir, dir.Parent);
            }
        }

        [Test]
        public void Should_Get_Files()
        {
            var parentDir = directory.GetDirectory("Files/FileSystem");
            var files = new List<IFile>(parentDir.GetFiles("*", System.IO.SearchOption.AllDirectories));

            Assert.AreEqual(4, files.Count);

            foreach (var file in files)
            {
                Assert.AreSame(parentDir, file.Directory);
            }
        }

        [Test]
        public void Should_Get_Full_Path()
        {
            Assert.AreEqual(root, directory.FullPath);
        }

        [Test]
        public void Should_Exist()
        {
            Assert.IsTrue(directory.Exists);
        }

        [Test]
        public void Should_Get_Attributes()
        {
            Assert.AreEqual(FileAttributes.Directory, directory.Attributes);
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
            var path = directory.GetAbsolutePath("~/dir/test.css");

            Assert.AreEqual(root + "/dir/test.css", path);
        }

        [Test]
        public void Should_Get_Absolute_Path_From_Relative()
        {
            var path = directory.GetAbsolutePath("test.css");

            Assert.AreEqual(root + "/test.css", path);
        }

        [Test]
        public void Should_Return_Normalized_Path_If_Already_Absolute()
        {
            var path = directory.GetAbsolutePath(root + "\\test.css");

            Assert.AreEqual(root + "/test.css", path);
        }

        [Test]
        public void Should_Get_Directory_By_Virtual_Path()
        {
            //all virtuals should be based on the root, regardless of heirarchy

            var dir1 = directory.GetDirectory("~/Test/");
            var dir2 = dir1.GetDirectory("~/TestTwo/Inner");

            Assert.AreEqual(root + "/Test", dir1.FullPath);
            Assert.AreEqual(root + "/TestTwo/Inner", dir2.FullPath);
        }

        [Test]
        public void Should_Get_Directory_By_Relative_Path()
        {
            //relative paths should get the directory as if it where inside the current directory

            var dir1 = directory.GetDirectory("Test/");
            var dir2 = dir1.GetDirectory("TestTwo/Inner/");

            Assert.AreEqual(root + "/Test", dir1.FullPath);
            Assert.AreEqual(root + "/Test/TestTwo/Inner", dir2.FullPath);
        }

        [Test]
        public void Should_Get_Directory_By_Absolute_Path()
        {
            //relative paths should get the directory as if it where inside the current directory

            var dir1 = directory.GetDirectory(root + "/Test");
            var dir2 = dir1.GetDirectory(root + "/TestTwo/Inner");

            Assert.AreEqual(root + "/Test", dir1.FullPath);
            Assert.AreEqual(root + "/TestTwo/Inner", dir2.FullPath);
        }

        [Test]
        public void Should_Get_Current_Directory_If_Path_Emtpy()
        {
            var dir = directory.GetDirectory("");

            Assert.AreSame(dir, directory);
        }

        [Test]
        public void Should_Get_File_By_Virtual_Path()
        {
            var file = directory.GetFile("~/Dir/Test.css");

            Assert.AreEqual(root + "/Dir/Test.css", file.Path);
        }

        [Test]
        public void Should_Get_File_By_Relative_Path()
        {
            var file = directory.GetFile("Dir/Test.css");

            Assert.AreEqual(root + "/Dir/Test.css", file.Path);
        }

        [Test]
        public void Should_Get_File_By_Absolute_Path()
        {
            var file = directory.GetFile(root + "/Dir/Test.css");

            Assert.AreEqual(root + "/Dir/Test.css", file.Path);
        }
    }
}
