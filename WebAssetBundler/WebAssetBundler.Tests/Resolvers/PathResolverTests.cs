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
    using System;
    using Moq;
    using System.Web;

    [TestFixture]
    public class PathResolverTests
    {
        [Test]
        public void Should_Combine_Into_Path()
        {
            var resolver = new PathResolver(WebAssetType.StyleSheet);
            var path = resolver.Resolve("path", "1.1", "test");

            Assert.AreEqual("path\\1.1\\test.css", path);
        }


        [Test]
        public void Should_Combine_Without_Version()
        {
            var resolver = new PathResolver(WebAssetType.StyleSheet);

            var path = resolver.Resolve("path", "", "test");
            Assert.AreEqual("path\\test.css", path);

            path = resolver.Resolve("path", null, "test");
            Assert.AreEqual("path\\test.css", path);
        }
    }
}
