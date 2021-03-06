﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

    [TestFixture]
    public class DirectorySearchOrderBuilderTests
    {
        private DirectorySearchOrderBuilder builder;
        private DirectorySearch directorySearch;

        [SetUp]
        public void Setup()
        {
            directorySearch = new DirectorySearch();
            builder = new DirectorySearchOrderBuilder(directorySearch);
        }

        [Test]
        public void Should_Add_First()
        {
            var returnBuilder = builder.First("file.js");

            Assert.AreEqual("file.js", directorySearch.OrderPatterns[0]);
            Assert.IsInstanceOf<DirectorySearchOrderBuilder>(returnBuilder);
        }

        [Test]
        public void Should_Add_Next()
        {
            var returnBuilder = builder.Next("file.js");
            builder.Next("file-two.js");

            Assert.AreEqual("file.js", directorySearch.OrderPatterns[0]);
            Assert.AreEqual("file-two.js", directorySearch.OrderPatterns[1]);
            Assert.IsInstanceOf<DirectorySearchOrderBuilder>(returnBuilder);
        }
    }
}
