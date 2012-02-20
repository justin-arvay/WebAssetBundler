using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ResourceCompiler.Assets;
using Moq;
using System.IO;


namespace Tests.Assets
{
    [TestFixture]
    public class PathOnlyBuilderTests
    {
        public Mock<IJavaScriptAssetsBuilder> assetBuilderMock;

        [SetUp]
        public void SetUp()
        {
            assetBuilderMock = new Mock<IJavaScriptAssetsBuilder>();
        }

        [Test]
        public void Can_Combine_Relative_Paths()
        {

            var builder = new PathOnlyBuilder("path", assetBuilderMock.Object);
            builder.Add("file.js");
            
            //Ensure the path is combined and passed to add. Note (\\) is escaping
            assetBuilderMock.Verify(x => x.Add(It.Is<string>(s => s.Equals("path\\file.js"))));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void File_Path_Cannot_Be_Rooted()
        {
            var builder = new PathOnlyBuilder("", assetBuilderMock.Object);
            builder.Add("\file.js");
        }

        [Test]
        public void Add_Method_Returns_Self_For_Chaining()
        {
            var builder = new PathOnlyBuilder("", assetBuilderMock.Object);
            Assert.IsInstanceOf(typeof(PathOnlyBuilder), builder.Add("file.js"));
        }

        //test Path action cannot be rooted
        //test path action returns for building
        //test path Add method returns self for building

    }
}
