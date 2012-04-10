
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class ImagePathContentFilterTests
    {
        [Test]
        public void Should_Transform_Relative_Path()
        {
            var filter = new ImagePathContentFilter(new Mock<IUrlResolver>().Object);

            Assert.AreSame("", filter.Filter("\\Generated/", "url(\"../img/test.jpg\");"));
        }


        [Test]
        public void Should_Transform_Relative_Path2()
        {
            var filter = new ImagePathContentFilter(new Mock<IUrlResolver>().Object);

            Assert.AreSame("", StyleSheetPathRewriter.RewriteCssPaths("C:/Generated/test.css", "~/Content/file.css", "url(\"../img/test.jpg\");"));
        }
    }
}
