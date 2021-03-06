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
    using System.IO;
    using System.Linq;

    [TestFixture]
    public class DirectorySearchBuilderTests
    {
        private DirectorySearchBuilder builder;
        private DirectorySearch context;

        [SetUp]
        public void Setup()
        {
            context = new DirectorySearch();
            builder = new DirectorySearchBuilder(context);
        }

        [Test]
        public void Should_Set_Pattern()
        {
            var returnBuilder = builder.AddPattern("*.js");

            Assert.AreEqual("*.js", context.Patterns.ToList()[0]);
            Assert.IsInstanceOf<DirectorySearchBuilder>(returnBuilder);
        }

        [Test]
        public void Should_Set_SearchOption()
        {
            var returnBuilder = builder.SearchOption(SearchOption.TopDirectoryOnly);

            Assert.AreEqual(SearchOption.TopDirectoryOnly, context.SearchOption);
            Assert.IsInstanceOf<DirectorySearchBuilder>(returnBuilder);
        }

        [Test]
        public void Should_Set_Order()
        {
            var returnBuilder = builder.Order(o => o
                .First("test.js"));

            Assert.AreEqual(1, context.OrderPatterns.Count);
            Assert.IsInstanceOf<DirectorySearchBuilder>(returnBuilder);
        }
    }
}
