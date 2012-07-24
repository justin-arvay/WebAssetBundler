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
    using WebAssetBundler.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class StyleSheetManagerTests
    {
        private WebAssetGroupCollection collection;
        private StyleSheetManager manager;

        [SetUp]
        public void Setup()
        {
            collection = new WebAssetGroupCollection();
            manager = new StyleSheetManager(collection);
        }

        [Test]
        public void Can_Get_Default_Group()
        {
            Assert.IsInstanceOf<WebAssetGroup>(manager.DefaultGroup);
        }       

        [Test]
        public void Should_Add_Default_Group_To_Collection()
        {
            Assert.AreEqual(1, collection.Count);
            Assert.True(collection.FindGroupByName(DefaultSettings.DefaultGroupName).Name.IsCaseSensitiveEqual(DefaultSettings.DefaultGroupName));
        }
    }
}
