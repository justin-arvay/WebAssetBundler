using System;
using System.Text;

namespace ResourceCompiler.Caching.Tests
{
    using System;
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class CacheTest
    {
        private Mock<ICacheProvider> provider;
        private Cache cache;

        private const string prefix = "prefix";
        private const string key = "key";
        private const string value = "value";

        public CacheTest()
        {
            provider = new Mock<ICacheProvider>();
            cache = new Cache(prefix, provider.Object);
        }

        [Test]
        public void Should_Get_Item_From_Provider()
        {
            provider.Setup(p => p.Get(cache.Prefix(key))).Returns(value);

            Assert.AreEqual(value, cache.Get<string>(key, null));
        }

        [Test]
        public void Getting_An_Item_Should_Call_Factory_Method_If_Provider_Returns_Null()
        {
            Assert.AreEqual(value, cache.Get(key, () => value));
        }

        [Test]
        public void Getting_An_Item_Should_Store_Value_Returned_From_Factory_In_Provider()
        {
            provider.Setup(p => p.Insert(cache.Prefix(key), value));

            cache.Get<string>(key, () => value);

            provider.VerifyAll();
        }

        [Test]
        public void Insert_Calls_Provider_Insert()
        {
            provider.Setup(p => p.Insert(cache.Prefix(key), value));

            cache.Insert(key, value);

            provider.VerifyAll();
        }

        [Test]
        public void Trying_To_Get_Value_Returns_False_When_Provider_Returns_Null()
        {
            //set up provider to return null when Get is called
            provider.Setup(p => p.Get(It.IsAny<string>())).Returns(null);

            string value;
            var result = cache.TryGetValue("foo", out value);

            Assert.False(result);
        }

        [Test]
        public void Trying_To_Get_Value_Sets_Result_To_Default_When_Provder_Returns_Null()
        {
            //set up provider to return null when Get is called
            provider.Setup(p => p.Get(It.IsAny<string>())).Returns(null);

            int value;
            var result = cache.TryGetValue("foo", out value);

            Assert.AreEqual(0, value);
        }

        [Test]
        public void Trying_To_Get_Value_Returns_True_When_Provider_Returns_Value()
        {
            //set up provider to return a value when Get is called
            provider.Setup(p => p.Get(cache.Prefix(key))).Returns(value);

            string valueOut;
            var result = cache.TryGetValue(key, out valueOut);

            Assert.True(result);
        }

        [Test]
        public void Trying_To_Get_Value_Sets_Result_When_Provider_Returns_Value()
        {
            //set up provider to return a value when Get is called
            provider.Setup(p => p.Get(cache.Prefix(key))).Returns(value);

            string valueOut;
            var result = cache.TryGetValue(key, out valueOut);

            Assert.AreEqual(value, valueOut);
        }
    }
}
