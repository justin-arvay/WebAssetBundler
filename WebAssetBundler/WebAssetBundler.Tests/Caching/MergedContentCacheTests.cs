// Web Asset Bundler - Bundles web assets so you dont have to.
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

    [TestFixture]
    public class MergedContentCacheTests
    {
        private Mock<ICacheProvider> provider;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ICacheProvider>();
        }

        [Test]
        public void Should_Add_Style_Sheet_To_Cache()
        {
            var cache = new MergedContentCache(WebAssetType.StyleSheet, provider.Object);

            cache.Add("name", "content");

            provider.Verify(p => p.Insert(
                It.Is<string>(s => s.Equals("MergedContent->StyleSheet->name")), 
                It.Is<string>(s => s.Equals("content"))), 
                Times.Once());
        }

        [Test]
        public void Should_Add_Script_To_Cache()
        {
            var cache = new MergedContentCache(WebAssetType.Script, provider.Object);

            cache.Add("name", "content");

            provider.Verify(p => p.Insert(
                It.Is<string>(s => s.Equals("MergedContent->Script->name")),
                It.Is<string>(s => s.Equals("content"))),
                Times.Once());
        }

        [Test]
        public void Should_Get_Style_Sheet_Content_From_Cache()
        {
            var cache = new MergedContentCache(WebAssetType.StyleSheet, provider.Object);

            cache.Get("name");

            provider.Verify(p => p.Get(
                It.Is<string>(s => s.Equals("MergedContent->StyleSheet->name"))),
                Times.Once());
        }

        [Test]
        public void Should_Get_Script_Content_From_Cache()
        {
            var cache = new MergedContentCache(WebAssetType.Script, provider.Object);

            cache.Get("name");

            provider.Verify(p => p.Get(
                It.Is<string>(s => s.Equals("MergedContent->Script->name"))),
                Times.Once());
        }

        [Test]
        public void Should_Return_Empty_String_When_Cache_Does_Not_Exist()
        {
            var cache = new MergedContentCache(WebAssetType.None, provider.Object);

            provider.Setup(p => p.Get(
                It.Is<string>(s => s.Equals("MergedContent->Script->name")))).Returns(null);

            var content = cache.Get("name");

            Assert.AreEqual("", content);
        }
    }
}
