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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using WebAssetBundler.Web.Mvc;


    [TestFixture]
    public class WebAssetGroupBuilderTests
    {
        private WebAssetGroupCollection sharedGroups;

        [SetUp]
        public void SetUp()
        {
            sharedGroups = new WebAssetGroupCollection();
        }

        [Test]
        public void Can_Add_Resource_To_Group()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            builder.Add("test.js");

            Assert.AreEqual(1, group.Assets.Count);

        }

        [Test]
        public void Add_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Add("test.js"));
        }

        [Test]
        public void Can_Enable_Or_Disable_Group()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            builder.Enable(true);
            Assert.True(group.Enabled, "Enabled Group");

            builder.Enable(false);
            Assert.False(group.Enabled, "Disabled Group");
        }

        [Test]
        public void Enable_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Enable(true));
        }

        [Test]
        public void Can_Enable_Or_Disable_Compression()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            builder.Compress(true);
            Assert.True(group.Compress, "Enabled Compression");

            builder.Compress(false);
            Assert.False(group.Compress, "Disabled Compression");
        }

        [Test]
        public void Compress_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Compress(true));
        }

        [Test]
        public void Can_Enable_Or_Disable_Combining()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            builder.Combine(true);
            Assert.True(group.Combine, "Enabled Combining");

            builder.Combine(false);
            Assert.False(group.Combine, "Disabled Combining");
        }

        [Test]
        public void Combine_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Combine(true));
        }

        [Test]
        public void Can_Set_Version()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            builder.Version("1.1.1");

            Assert.AreEqual("1.1.1", group.Version);
        }

        [Test]
        public void Version_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Version("1.1"));
        }

        [Test]
        public void Should_Add_Assets_From_Shared()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            var sharedGroup = new WebAssetGroup("Test", true, "");
            sharedGroup.Assets.Add(new WebAsset("~/Test/File.css"));
            sharedGroups.Add(sharedGroup);

            builder.AddShared("Test");

            Assert.AreEqual(1, group.Assets.Count);   
        }

        [Test]
        public void Should_Return_Self_For_Chaining_When_Adding_Shared()
        {
            var group = new WebAssetGroup("", false, "");
            var builder = new WebAssetGroupBuilder(group, sharedGroups);

            sharedGroups.Add(new WebAssetGroup("Test", true, ""));

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.AddShared("Test"));
        }
    }
}
