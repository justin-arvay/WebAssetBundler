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
    using NUnit.Framework;
    using System;
    using ResourceCompiler.Web.Mvc;
    using System.Collections.Generic;

    [TestFixture]
    public class WebAssetGroupResolverTests
    {

        [Test]
        public void Should_Resolve_A_List_Of_Sources_For_Each_Item_In_Group()
        {
            var group = new WebAssetGroup("Test", false);
            group.Assets.Add(new WebAsset("~/Files/test.css"));
            group.Assets.Add(new WebAsset("~/Files/test2.css"));

            var resolver = new WebAssetGroupResolver(group);

            Assert.AreEqual(2, resolver.Resolve().Count);
        }

        [Test]
        public void Should_Use_Source_For_Result_Path()
        {
            var path = "~/Files/test.css";
            var group = new WebAssetGroup("Test", false);

            group.Assets.Add(new WebAsset(path));

            var resolver = new WebAssetGroupResolver(group);
            var results = resolver.Resolve();

            Assert.AreEqual(path, results[0].Path);
        }

        [Test]
        public void Should_Resolve_Compress_For_Result()
        {            
            var group = new WebAssetGroup("Test", false);

            group.Compress = true;
            group.Assets.Add(new WebAsset(""));

            var resolver = new WebAssetGroupResolver(group);
            var results = resolver.Resolve();

            Assert.IsTrue(results[0].Compress);
        }
    }
}
