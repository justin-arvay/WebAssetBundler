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
    public class WebAssetGeneratorTests
    {
        private Mock<IMergedResultCache> cache;

        [SetUp]
        public void Setup()
        {
            cache = new Mock<IMergedResultCache>();
        }

        [Test]
        public void Should_Generate_If_No_Cache_Exists()
        {
            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object, cache.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));
            results.Add(new WebAssetResolverResult("", false, null));

            generator.Generate(results);

            writer.Verify(w => w.Write(It.IsAny<WebAssetMergerResult>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Merge_Each_Result()
        {
            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object, cache.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));
            results.Add(new WebAssetResolverResult("", false, null));

            generator.Generate(results);

            merger.Verify(w => w.Merge(It.IsAny<WebAssetResolverResult>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Cache_Merged_Result()
        {
            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object, cache.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));

            generator.Generate(results);
            cache.Verify(c => c.Add(It.IsAny<WebAssetMergerResult>()), Times.Once());
        }

        [Test]
        public void Should_Not_Cache_Result()
        {
            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object, cache.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));

            cache.Setup(c => c.Exists(It.IsAny<WebAssetMergerResult>()))
                .Returns(true);

            generator.Generate(results);

            //should not add it if it exists
            cache.Verify(c => c.Add(It.IsAny<WebAssetMergerResult>()), Times.Never());
        }

        [Test]
        public void Should_Not_Generate_If_Cache_Exists()
        {

            var merger = new Mock<IWebAssetMerger>();
            var writer = new Mock<IWebAssetWriter>();
            var generator = new WebAssetGenerator(writer.Object, merger.Object, cache.Object);

            var results = new List<WebAssetResolverResult>();
            results.Add(new WebAssetResolverResult("", false, null));

            cache.Setup(c => c.Exists(It.IsAny<WebAssetMergerResult>()))
                .Returns(true);

            generator.Generate(results);

            //should not add it if it exists
            writer.Verify(w => w.Write(It.IsAny<WebAssetMergerResult>()), Times.Never());
        }
    }
}
