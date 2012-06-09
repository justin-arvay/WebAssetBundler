// WebAssetBundler - Bundles web assets so you dont have to.
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
