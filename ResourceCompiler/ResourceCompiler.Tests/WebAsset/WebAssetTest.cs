namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;

    [TestFixture]
    public class WebAssetTest
    {

        [Test]
        public void Should_Be_Able_To_Get_Source_Set_By_Constructor()
        {
            var source = "source/file.jpg";
            var webAsset = new WebAsset(source);

            Assert.AreEqual(source, webAsset.Source);
        }
    }
}
