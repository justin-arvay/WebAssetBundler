
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
            string outputPath = "C:\\Content\\test.css";
            string content = "url(\"../img/test.jpg\");";
            
            var filter = new ImagePathContentFilter();

            Assert.AreEqual("url(\"../img/test.jpg\");", filter.Filter(outputPath, sourcePath, content));
        }

        [Test]
        public void Should_Filter_With_Different_Directory_Names()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Generated\\Sub\\file.css";
            string content = "url(\"../img/test.jpg\");";

            var filter = new ImagePathContentFilter();

            Assert.AreEqual("url(\"../../img/test.jpg\");", filter.Filter(outputPath, sourcePath, content));
        }

        [Test]
        public void Should_Filter_Without_Double_Quotes()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Content\\Sub\\file.css";
            string content = "url(../img/test.jpg);";

            var filter = new ImagePathContentFilter();

            Assert.AreEqual("url(../../img/test.jpg);", filter.Filter(outputPath, sourcePath, content));
        }

        [Test]
        public void Should_Filter_With_Single_Quotes()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Content\\Sub\\file.css";
            string content = "url('../img/test.jpg');";

            var filter = new ImagePathContentFilter();

            Assert.AreEqual("url('../../img/test.jpg');", filter.Filter(outputPath, sourcePath, content));
        }

        [Test]
        public void Should_Filter_With_Src()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Content\\Sub\\file.css";
            string content = "(src=\"../img/test.jpg\");";

            var filter = new ImagePathContentFilter();

            Assert.AreEqual("(src=\"../../img/test.jpg\");", filter.Filter(outputPath, sourcePath, content));
        }

        [Test]
        public void Should_Filter_With_Src_And_With_Single_Quotes()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Content\\Sub\\file.css";
            string content = "(src='../img/test.jpg');";

            var filter = new ImagePathContentFilter();

            Assert.AreEqual("(src='../../img/test.jpg');", filter.Filter(outputPath, sourcePath, content));
        }

        [Test]
        public void Should_Filter_With_Src_Without_Double_Quotes()
        {
            string sourcePath = "C:\\Content\\file.css";
            string outputPath = "C:\\Content\\Sub\\file.css";
            string content = "(src=../img/test.jpg);";

            var filter = new ImagePathContentFilter();

            Assert.AreEqual("(src=../../img/test.jpg);", filter.Filter(outputPath, sourcePath, content));
        }
    }
}
