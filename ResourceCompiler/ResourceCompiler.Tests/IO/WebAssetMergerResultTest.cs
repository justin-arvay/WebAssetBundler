

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class WebAssetMergerResultTest
    {
        [Test]
        public void Should_Set_Path_With_Constructor()
        {
            var path = "some/path/file.css";
            var result = new WebAssetMergerResult(path, "");

            Assert.AreSame(result.Path, path);
        }

        [Test]
        public void Should_Set_Content_With_Constructor()
        {
            var content = "some content";
            var result = new WebAssetMergerResult("", content);

            Assert.AreSame(result.Content, content);
        }
    }
}
