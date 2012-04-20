
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System;

    [TestFixture]
    public class MergedResultCacheTests
    {
        private Mock<ICacheProvider> provider;
        private IMergedResultCache cache;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ICacheProvider>();
            cache = new MergedResultCache(provider.Object);
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
    }
}
