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
        private IMergedResultCache cache;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ICacheProvider>();
            cache = new MergedResultCache(WebAssetType.None, provider.Object);
        }

        [Test]
        public void Should_Add_Result_To_Cache()
        {
            cache.Add(new WebAssetMergerResult("path/file.css", ""));

            provider.Verify(p => p.Insert(It.IsAny<string>(), It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void Should_Exist_If_Result_In_Cache()
        {
            var result = new WebAssetMergerResult("", "");            

            provider.Setup(p => p.Get(It.IsAny<string>()))
                .Returns(result);

            Assert.IsTrue(cache.Exists(result));
        }

        [Test]
        public void Should_Throw_Exception_If_Cannot_Add_To_Cache()
        {
            Assert.Throws<InvalidOperationException>(() => cache.Add(new WebAssetMergerResult("", "")));
        }

        [Test]
        public void Should_Add_Many_Different_Results_To_Cache()
        {
            cache.Add(new WebAssetMergerResult("path/file.css", ""));
            cache.Add(new WebAssetMergerResult("path/file2.css", ""));

            provider.Verify(p => p.Insert(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Not_Exist_In_Cache()
        {
            var result = new WebAssetMergerResult("path/file.css", "");

            provider.Setup(p => p.Get(It.IsAny<string>()))
                .Returns(null);

            Assert.IsFalse(cache.Exists(result));

        }

        [Test]
        public void Should_Add_To_Cache_With_Unique_Key_Per_Type()
        {
            var result = new WebAssetMergerResult("path/file.ext", "");
            var jsResultCache = new MergedResultCache(WebAssetType.Script, provider.Object);
            var cssResultCache = new MergedResultCache(WebAssetType.StyleSheet, provider.Object);

            var paths = new List<string>();

            provider.Setup(p => p.Insert(It.IsAny<string>(), It.IsAny<object>()))
                .Callback((string path, object mergedResult) => {
                    paths.Add(path);
                });

            jsResultCache.Add(result);
            cssResultCache.Add(result);

            //should return 1 path for each result added
            Assert.AreEqual(2, paths.Distinct().Count());
        }
    }
}
