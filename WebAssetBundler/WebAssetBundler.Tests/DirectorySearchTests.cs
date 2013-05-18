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
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    [TestFixture]
    public class DirectorySearchTests
    {
        private DirectorySearch directorySearch;
        private Mock<IDirectory> directory;

        [SetUp]
        public void Setup()
        {
            directorySearch = new DirectorySearch();
            directory = new Mock<IDirectory>();
        }


        [Test]
        public void Should_Set_Defaults()
        {
            Assert.AreEqual(SearchOption.AllDirectories, directorySearch.SearchOption);
            Assert.NotNull(directorySearch.Patterns);
        }

        [Test]
        public void Should_Find_Files()
        {
            directorySearch.Patterns.Add("*.js");

            var files = new List<IFile>()
            {
                new File("jquery.js"),
                new File("jquery-ui.js")
            };

            directory.Setup(d => d.GetFiles(It.IsAny<string>(), SearchOption.AllDirectories))
                .Returns(files);

            IList<IFile> returnFiles = directorySearch.FindFiles(directory.Object).ToList();

            Assert.AreEqual("jquery.js", returnFiles[0].Path);
            Assert.AreEqual("jquery-ui.js", returnFiles[1].Path);
        }

        [Test]
        public void Should_Order_Files()
        {
            directorySearch.OrderPatterns.Add("jquery.js");
            directorySearch.OrderPatterns.Add("jquery-ui.js");

            var files = new List<IFile>()
            {
                new File("/Dir/first-but-shouldnt-be.js"),
                new File("/Dir/jquery.js"),
                new File("/Dir/jquery-ui.js"),
                new File("/Dir/something-else.js")
            };

            IList<IFile> returnFiles = directorySearch.OrderFiles(files).ToList();

            Assert.AreEqual("/Dir/jquery.js", returnFiles[0].Path);
            Assert.AreEqual("/Dir/jquery-ui.js", returnFiles[1].Path);
            Assert.AreEqual("/Dir/first-but-shouldnt-be.js", returnFiles[2].Path);
            Assert.AreEqual("/Dir/something-else.js", returnFiles[3].Path);
        }

        [Test]
        public void Should_Order_Files_With_Wildcard()
        {
            directorySearch.OrderPatterns.Add("jquery*");
            directorySearch.OrderPatterns.Add("jquery-ui*");
            directorySearch.OrderPatterns.Add("jquery*ui*");

            var files = new List<IFile>()
            {
                new File("/Dir/jquery-tree.js"),
                new File("/Dir/jquery-ui.js"),
                new File("/Dir/test.js"),
                new File("/Dir/jquery.js")                               
            };

            IList<IFile> returnFiles = directorySearch.OrderFiles(files).ToList();

            Assert.AreEqual("/Dir/jquery.js", returnFiles[0].Path);
            Assert.AreEqual("/Dir/jquery-tree.js", returnFiles[1].Path);
            Assert.AreEqual("/Dir/jquery-ui.js", returnFiles[2].Path);
            Assert.AreEqual("/Dir/test.js", returnFiles[3].Path);            
        }

        [Test]
        public void Should_Order_Files_With_Sub_Directory_In_Pattern()
        {
            Assert.Fail();
        }

        private class File : IFile
        {
            private string path;

            public File(string path)
            {
                this.path = path;
            }

            public bool Exists
            {
                get { throw new NotImplementedException(); }
            }

            public string Path
            {
                get 
                {
                    return path;
                }
            }

            public IDirectory Directory
            {
                get { throw new NotImplementedException(); }
            }

            public Stream Open(FileMode mode)
            {
                throw new NotImplementedException();
            }

            public Stream Open(FileMode mode, FileAccess access)
            {
                throw new NotImplementedException();
            }

            public Stream Open(FileMode mode, FileAccess access, FileShare fileShare)
            {
                throw new NotImplementedException();
            }
        }
    }
}
