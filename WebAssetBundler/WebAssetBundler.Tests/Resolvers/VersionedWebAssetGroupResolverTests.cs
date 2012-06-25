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
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class VersionedWebAssetGroupResolverTests
    {
        [Test]
        public void Should_Return_A_List_Of_Results()
        {
            var group = new WebAssetGroup("Test", false);

            group.Assets.Add(new WebAsset("~/Files/test.css"));
            group.Assets.Add(new WebAsset("~/Files/test2.css"));

            var resolver = new VersionedWebAssetGroupResolver(group);

            Assert.AreEqual(2, resolver.Resolve().Count);
        }        

        [Test]
        public void Should_Resolve_Compress_For_Result()
        {
            var group = new WebAssetGroup("Test", false) { Compress = true };
            
            group.Assets.Add(new WebAsset("~/Files/test.css"));            

            var resolver = new VersionedWebAssetGroupResolver(group);
            var results = resolver.Resolve();

            Assert.IsTrue(results[0].Compress);
        }

        [Test]
        public void Should_Resolve_Version_For_Result()
        {
            var group = new WebAssetGroup("Test", false) { Version = "1.2" };
            
            group.Assets.Add(new WebAsset("~/Files/test.css"));

            var resolver = new VersionedWebAssetGroupResolver(group);
            var results = resolver.Resolve();

            Assert.AreEqual("1.2", results[0].Version);
        }

        [Test]
        public void Should_Resolve_Name_For_Result()
        {
            var group = new WebAssetGroup("Test", false);
            
            group.Assets.Add(new WebAsset("~/Files/the-file.css"));

            var resolver = new VersionedWebAssetGroupResolver(group);
            var results = resolver.Resolve();

            Assert.AreEqual("the-file", results[0].Name);
        }
    }
}
