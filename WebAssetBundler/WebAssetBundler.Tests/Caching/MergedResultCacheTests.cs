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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class MergedResultCacheTests
    {
        private Mock<ICacheProvider> provider;
        private IMergedResultCache cssCache;
        private IMergedResultCache jsCache;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ICacheProvider>();
            cssCache = new MergedResultCache(WebAssetType.StyleSheet, provider.Object);
            jsCache = new MergedResultCache(WebAssetType.Script, provider.Object);
        }

        [Test]
        public void Should_Add_Result_To_Cache()
        {
            cssCache.Add(new MergerResult("Name", "", WebAssetType.StyleSheet));

            provider.Verify(p => p.Insert(
                "MergedResult->StyleSheet->Name", 
                It.IsAny<MergerResult>()
            ), Times.Once());

            jsCache.Add(new MergerResult("Name", "", WebAssetType.Script));

            provider.Verify(p => p.Insert(
                "MergedResult->Script->Name",
                It.IsAny<MergerResult>()
            ), Times.Once());
        }

        [Test]
        public void Should_Add_Many_Different_Results_To_Cache()
        {
            cssCache.Add(new MergerResult("Name1", "", WebAssetType.None));
            cssCache.Add(new MergerResult("Name2", "", WebAssetType.None));

            provider.Verify(p => p.Insert(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Not_Exist_In_Cache()
        {
            var result = new MergerResult("Name", "", WebAssetType.None);

            provider.Setup(p => p.Get(It.IsAny<string>()))
                .Returns(null);

            Assert.IsNull(cssCache.Get("Name"));

        }

        [Test]
        public void Should_Add_To_Cache_With_Unique_Key_Per_Type()
        {
            var result = new MergerResult("Name", "", WebAssetType.None);
            var paths = new List<string>();

            provider.Setup(p => p.Insert(It.IsAny<string>(), It.IsAny<object>()))
                .Callback((string path, object mergedResult) => {
                    paths.Add(path);
                });

            jsCache.Add(result);
            cssCache.Add(result);

            //should return 1 path for each result added
            Assert.AreEqual(2, paths.Distinct().Count());
        }

        [Test]
        public void Should_Get_Result()
        {
            var result = jsCache.Get("Name");

            provider.Verify(p => p.Get("MergedResult->Script->Name"), Times.Once());
        }
    }
}
