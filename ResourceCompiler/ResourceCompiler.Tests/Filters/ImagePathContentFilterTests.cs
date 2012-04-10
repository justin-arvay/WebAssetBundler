
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class ImagePathContentFilterTests
    {
        [Test]
        public void Should_Filter_With_Different_File_Names()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Generated\\test.css";
            string content = "url(\"../img/test.jpg\");";
            
            var filter = new ImagePathContentFilter();

            Assert.AreSame("", filter.Filter(outputPath, sourcePath, content));
        }
    }
}
