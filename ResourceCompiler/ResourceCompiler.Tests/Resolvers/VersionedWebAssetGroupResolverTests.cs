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
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class VersionedWebAssetGroupResolverTests
    {
        [Test]
        public void Should_Return_A_List_Of_Results()
        {
            var pathResolver = new Mock<IPathResolver>();
            var group = new WebAssetGroup("Test", false, "")
            {
                Version = "1.2"
            };

            group.Assets.Add(new WebAsset("~/Files/test.css"));
            group.Assets.Add(new WebAsset("~/Files/test2.css"));

            var resolver = new VersionedWebAssetGroupResolver(group, pathResolver.Object);

            Assert.AreEqual(2, resolver.Resolve().Count);
        }

        [Test]
        public void Should_Resolve_Path_For_Result()
        {
            var pathResolver = new Mock<IPathResolver>();
            var path = "/test/file.css";
            var group = new WebAssetGroup("Test", false, DefaultSettings.GeneratedFilesPath) { Version = "1.2" };

            group.Assets.Add(new WebAsset(path));     
      
            pathResolver.Setup(m => m.Resolve(
                It.Is<string>(s => s.Equals(DefaultSettings.GeneratedFilesPath)),
                It.Is<string>(s => s.Equals(group.Version)),
                It.Is<string>(s => s.Equals("file"))))                
                .Returns(path);

            var resolver = new VersionedWebAssetGroupResolver(group, pathResolver.Object);
            var results = (List<WebAssetResolverResult>)resolver.Resolve();

            Assert.AreEqual(path, results[0].Path);
        }

        [Test]
        public void Should_Resolve_Compress_For_Result()
        {
            var pathResolver = new Mock<IPathResolver>();
            var path = "/test/file.css";
            var group = new WebAssetGroup("Test", false, "") { Version = "1.2" };

            group.Compress = true;
            group.Assets.Add(new WebAsset(path));            

            var resolver = new VersionedWebAssetGroupResolver(group, pathResolver.Object);
            var results = resolver.Resolve();

            Assert.IsTrue(results[0].Compress);
        }

        [Test]
        public void Should_Pass_Correct_Prams_When_Resolving()
        {
            var pathResolver = new Mock<IPathResolver>();
            var path = "/test/file.css";
            var group = new WebAssetGroup("Test", false, DefaultSettings.GeneratedFilesPath) { Version = "1.2" };

            group.Assets.Add(new WebAsset(path));

            var resolver = new VersionedWebAssetGroupResolver(group, pathResolver.Object);
            resolver.Resolve();

            pathResolver.Verify(m => m.Resolve(
                It.Is<string>(s => s.Equals(DefaultSettings.GeneratedFilesPath)),
                It.Is<string>(s => s.Equals(group.Version)),
                It.Is<string>(s => s.Equals("file"))));
        }
    }
}
