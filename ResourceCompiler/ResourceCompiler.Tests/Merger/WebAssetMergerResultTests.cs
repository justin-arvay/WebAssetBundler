namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WebAssetMergerResultTests
    {

        [Test]
        public void SHould_Set_Content_In_Constructor()
        {
            var content = "test content";
            var result = new WebAssetMergerResult("", "", content);

            Assert.AreEqual(content, result.Content);
        }

        [Test]
        public void Should_Set_Version_In_Constructor()
        {
            var version = "1.1";
            var result = new WebAssetMergerResult("", version, "");

            Assert.AreEqual(version, result.Version);
        }

        [Test]
        public void Should_Set_Name_In_Constructor()
        {
            var name = "testname";
            var result = new WebAssetMergerResult(name, "", "");

            Assert.AreEqual(name, result.Name);
        }
    }
}
