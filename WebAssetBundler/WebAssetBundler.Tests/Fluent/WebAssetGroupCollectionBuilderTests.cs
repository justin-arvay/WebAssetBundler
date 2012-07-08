﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
    using WebAssetBundler.Web.Mvc;

    [TestFixture]
    public class WebAssetGroupCollectionBuilderTests
    {
        private WebAssetGroupCollection sharedGroups;
        private WebAssetGroupCollection collection;

        [SetUp]
        public void Setup()
        {
            sharedGroups = new WebAssetGroupCollection();
            collection = new WebAssetGroupCollection();
        }

        [Test]
        public void Should_Be_Able_To_Add_Group()
        {            
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            builder.AddGroup("test", g => g.ToString());

            Assert.AreEqual(1, collection.Count);
        }                

        [Test]
        public void Should_Throw_Exception_When_Adding_Group_That_Already_Exists()
        {            
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            builder.AddGroup("test", g => g.ToString());

            Assert.Throws<ArgumentException>(() => builder.AddGroup("test", g => g.ToString()));           
        }

        [Test]
        public void Should_Have_Nothing_In_Collection_By_Default()
        {            
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Adding_File_Should_Add_New_Group_With_Asset()
        {            
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

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
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.AddGroup("test", g => g.ToString()));
        }

        [Test]
        public void Add_Should_Return_Self_For_Chaining()
        {            
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.Add("~/Files/test.css"));
        }

        [Test]
        public void Adding_Shared_Group_Should_Return_Self_For_Chaining()
        {
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);
            sharedGroups.Add(new WebAssetGroup("Foo", true));

            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.AddSharedGroup("Foo"), "No Configuration");           
        }

        [Test]
        public void Adding_Configured_Shared_Group_Should_Return_Self_For_Chaining()
        {
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);
            sharedGroups.Add(new WebAssetGroup("Foo", true));
            
            Assert.IsInstanceOf<WebAssetGroupCollectionBuilder>(builder.AddSharedGroup("Foo", x => x.ToString()), "Configuration");
        }

        [Test]
        public void Should_Allow_Overriding_Of_Configuration_For_Shared_Group()
        {
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            //add the shared group with combine false
            sharedGroups.Add(new WebAssetGroup("Foo", true) { Combine = false });

            //override the shared group by setting combine to true
            builder.AddSharedGroup("Foo", g => g.Combine(true));

            Assert.IsTrue(sharedGroups.FindGroupByName("Foo").Combine);
        }

        [Test]
        public void Should_Add_Shared_Group_To_Groups()
        {
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);
            var group = new WebAssetGroup("Foo", true);
            sharedGroups.Add(group);

            builder.AddSharedGroup("Foo");

            Assert.AreSame(group, collection.FindGroupByName("Foo"));
        }

        [Test]
        public void Should_Throw_Exception_If_Shared_Group_Doesnt_Exist()
        {
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            Assert.Throws<ArgumentException>(() => builder.AddSharedGroup("name"), "No Configure");
            Assert.Throws<ArgumentException>(() => builder.AddSharedGroup("name", g => g.ToString()), "Configure");
        }

        [Test]
        public void Should_Set_Shared_Group_To_Included()
        {
            sharedGroups.Add(new WebAssetGroup("Foo", true));
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            builder.AddSharedGroup("Foo");

            Assert.IsTrue(sharedGroups[0].IsIncluded);
        }

        [Test]
        public void Should_Set_Configured_Shared_Group_To_Included()
        {
            sharedGroups.Add(new WebAssetGroup("Foo", true));
            var builder = new WebAssetGroupCollectionBuilder(collection, sharedGroups);

            builder.AddSharedGroup("Foo", s => s.ToString());

            Assert.IsTrue(sharedGroups[0].IsIncluded);
        }
    }
}