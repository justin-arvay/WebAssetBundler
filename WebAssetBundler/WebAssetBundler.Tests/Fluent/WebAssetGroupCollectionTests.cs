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
    using System;
    using NUnit.Framework;
    using Moq;
    using WebAssetBundler.Web.Mvc;

    [TestFixture]
    public class WebAssetGroupCollectionTests
    {
        private WebAssetGroupCollection collection;

        [SetUp]
        public void Setup()
        {
            collection = new WebAssetGroupCollection();
        }

        [Test]
        public void Should_Find_Group_By_Name()
        {
            
            collection.Add(new WebAssetGroup("test", false));

            Assert.NotNull(collection.FindGroupByName("test"));
        }

        [Test]
        public void Should_Find_Group_By_Name_Regardless_Of_Case()
        {
            collection.Add(new WebAssetGroup("Test", false));

            Assert.NotNull(collection.FindGroupByName("tEST"));
        }

        [Test]
        public void Should_Return_Null_If_Not_Group_Not_Found()
        {
            Assert.Null(collection.FindGroupByName("Foo"));
        }
    }
}
