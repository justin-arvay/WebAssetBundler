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

namespace WebAssetBundler.Web.Mvc
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class ResolverResultTests
    {
        [Test]
        public void Should_Set_Path_In_Constructor()
        {
            var path = "some/path/file.css";
            var result = new ResolverResult(path, false, null);

            Assert.AreEqual(path, result.Path);
        }

        [Test]
        public void Should_Set_Compress_In_Constructor()
        {
            var result = new ResolverResult("", true, null);

            Assert.IsTrue(result.Compress);
        }

        [Test]
        public void Should_Set_Web_Assets_In_Constructor()
        {
            var webAssets = new List<IWebAsset>();
            var result = new ResolverResult("", false, webAssets);

            Assert.NotNull(result.WebAssets);
        }
    }
}
