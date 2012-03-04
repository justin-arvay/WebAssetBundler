namespace Tests.Extensions
{
    using System;
    using NUnit.Framework;
    using ResourceCompiler.Extensions;

    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void Should_Be_Equal_Using_Case_Insensitive_Check()
        {
            string name = "John";

            Assert.True(name.IsCaseInsensitiveEqual("jOHN"));
        }

        [Test]
        public void Should_Be_Equal_Using_Case_Sensitive_Check()
        {
            string name = "John";

            Assert.True(name.IsCaseSensitiveEqual("John"));
        }
    }
}
