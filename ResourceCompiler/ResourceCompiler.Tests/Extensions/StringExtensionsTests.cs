namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;

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

        [Test]
        public void Should_Be_Able_To_Format_String()
        {
            string format = "{0} - {1}";
            Assert.AreEqual("t - s", format.FormatWith("t", "s"));
        }

        [Test]
        public void Should_Find_Correct_Number_Of_Occurances()
        {
            string value = "This is the best string.";

            Assert.AreEqual(2, value.CountOccurance("is"));
        }
    }
}
