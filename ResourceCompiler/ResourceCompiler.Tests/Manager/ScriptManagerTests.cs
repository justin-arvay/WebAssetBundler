﻿// ResourceCompiler - Compiles web assets so you dont have to.
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

    [TestFixture]
    public class ScriptManagerTests
    {
        [Test]
        public void Can_Get_Default_Group()
        {
            var manager = new ScriptManager(new WebAssetGroupCollection());

            Assert.IsInstanceOf<WebAssetGroup>(manager.DefaultGroup);
        }

        [Test]
        public void Default_Groups_Generated_Path_Is_Set_To_Default_Generated_Path_By_Default()
        {
            var manager = new ScriptManager(new WebAssetGroupCollection());

            Assert.AreEqual(DefaultSettings.GeneratedFilesPath, manager.DefaultGroup.GeneratedPath);
        }


        [Test]
        public void Should_Add_Default_Group_To_Collection()
        {
            var collection = new WebAssetGroupCollection();
            var manager = new ScriptManager(collection);

            Assert.AreEqual(1, collection.Count);
            Assert.True(collection.FindGroupByName(DefaultSettings.DefaultGroupName).Name.IsCaseSensitiveEqual(DefaultSettings.DefaultGroupName));
        }

    }
}
