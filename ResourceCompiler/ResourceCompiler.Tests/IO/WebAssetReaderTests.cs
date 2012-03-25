namespace ResourceCompiler.Web.Mvc
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WebAssetReaderTests
    {
        [Test]
        public void Should_Read_Content_From_Web_Asset()
        {
            var reader = new WebAssetReader();
            var webAsset = new WebAsset("Files/read.txt");

            Assert.AreEqual("Line 1", reader.Read(webAsset));
        }
    }
}
