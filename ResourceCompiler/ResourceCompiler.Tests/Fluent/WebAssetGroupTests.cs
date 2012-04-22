// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ResourceCompiler.Web.Mvc;
    using NUnit.Framework;


    [TestFixture]
    public class WebAsssetGroupTests
    {
        [Test]
        public void Constructor_Sets_Name()
        {
            var group = new WebAssetGroup("John", false, "");

            Assert.AreEqual("John", group.Name);
        }

        [Test]
        public void Constructor_Sets_IsShared()
        {
            var group = new WebAssetGroup("", true, "");

            Assert.True(group.IsShared);
        }

        [Test]
        public void Can_Set_Combined()
        {
            var group = new WebAssetGroup("", false, "");

            //get the opposite of its current value so we always know it changed
            var value = group.Combine ? false : true;
                        
            group.Combine = value;

            Assert.AreEqual(value, group.Combine);
        }

        [Test]
        public void Can_Set_Compressed()
        {
            var group = new WebAssetGroup("", false, "");

            //get the opposite of its current value so we always know it changed
            var value = group.Compress ? false : true;

            group.Compress = value;

            Assert.AreEqual(value, group.Compress);
        }

        [Test]
        public void Can_Set_Version()
        {
            var group = new WebAssetGroup("", false, "");

            group.Version = "2.0";

            Assert.AreEqual("2.0", group.Version);
        }

        [Test]
        public void Can_Set_Enabled()
        {
            var group = new WebAssetGroup("", false, "");

            //get the opposite of its current value so we always know it changed
            var value = group.Enabled ? false : true;

            group.Enabled = value;

            Assert.AreEqual(value, group.Enabled);
        }

        [Test]
        public void Constructor_Sets_GeneratedPath_Path()
        {
            var group = new WebAssetGroup("", false, "/path");            

            Assert.AreEqual("/path", group.GeneratedPath);
        }

        [Test]
        public void Resources_Are_Empty_By_Default()
        {
            var group = new WebAssetGroup("", false, "");

            Assert.AreEqual(0, group.Assets.Count);
        }

        [Test]
        public void Can_Add_Resource()
        {
            var group = new WebAssetGroup("", false, "");
            group.Assets.Add(new WebAsset("/path/file.js"));

            Assert.AreEqual(1, group.Assets.Count);
        }

        public void Adding_Duplicate_Item_Throws_Exception()
        {
            var group = new WebAssetGroup("", false, "");

            group.Assets.Add(new WebAsset("/path/file.js"));
            Assert.Throws<ArgumentException>(() => group.Assets.Add(new WebAsset("/path/file.js")));

        }

        public void Setting_Duplicate_Item_To_Existing_Index_Throws_Exception()
        {
            var group = new WebAssetGroup("", false, "");

            group.Assets.Add(new WebAsset("/path/file.js"));
            Assert.Throws<ArgumentException>(() => group.Assets.Insert(1, new WebAsset("/path/file.js")));
        }

    }
}
