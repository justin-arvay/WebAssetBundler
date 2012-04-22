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
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;

    [TestFixture]
    public class WebAssetGroupCollectionBuilderTests
    {
        [Test]
        public void Should_Be_Able_To_Add_Group()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            builder.AddGroup("test", g => g.ToString());

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void Should_Set_Path_Group_For_New_Group()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            builder.AddGroup("test", g => g.ToString());

            Assert.AreEqual(DefaultSettings.GeneratedFilesPath, collection[0].GeneratedPath);
        }

        [Test]
        public void Should_Set_Path_Group_For_Group_When_Adding_Single_Asset()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            builder.Add("~/soure/file.css");

            Assert.AreEqual(DefaultSettings.GeneratedFilesPath, collection[0].GeneratedPath);
        }

        [Test]
        public void Should_Throw_Exception_When_Adding_Group_That_Already_Exists()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            builder.AddGroup("test", g => g.ToString());

            Assert.Throws<ArgumentException>(() => builder.AddGroup("test", g => g.ToString()));           
        }

        [Test]
        public void Should_Have_Nothing_In_Collection_By_Default()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Adding_File_Should_Add_New_Group_With_Resource()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            builder.Add("~/Files/test.css");
            builder.Add("~/Files/test.css");

            //should be 2 items in the collection, and 1 item in each collections group
            Assert.AreEqual(2, collection.Count);
            foreach (var group in collection) 
            {
                Assert.AreEqual(1, group.Assets.Count);
            }
        }

        [Test]
        public void Adding_Group_Should_Return_Self_For_Chaining()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.AddGroup("test", g => g.ToString()));
        }

        [Test]
        public void Add_Should_Return_Self_For_Chaining()
        {
            var collection = new WebAssetGroupCollection();
            var builder = new WebAssetGroupCollectionBuilder(WebAssetType.None, collection, DefaultSettings.GeneratedFilesPath);

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.Add("~/Files/test.css"));
        }
    }
}
