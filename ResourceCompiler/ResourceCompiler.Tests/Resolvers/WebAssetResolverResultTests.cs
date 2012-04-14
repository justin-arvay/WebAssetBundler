
namespace ResourceCompiler.Web.Mvc
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class WebAssetResolverResultTests
    {
        [Test]
        public void Should_Set_Path_In_Constructor()
        {
            var path = "some/path/file.css";
            var result = new WebAssetResolverResult(path, false, null);

            Assert.AreEqual(path, result.Path);
        }

        [Test]
        public void Should_Set_Compress_In_Constructor()
        {
            var result = new WebAssetResolverResult("", true, null);

            Assert.IsTrue(result.Compress);
        }

        [Test]
        public void Should_Set_Web_Assets_In_Constructor()
        {
            var webAssets = new List<IWebAsset>();
            var result = new WebAssetResolverResult("", false, webAssets);

            Assert.NotNull(result.WebAssets);
        }
    }
}
