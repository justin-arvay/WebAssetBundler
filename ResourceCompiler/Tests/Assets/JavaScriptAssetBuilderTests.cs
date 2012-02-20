using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ResourceCompiler.Assets;
using Moq;



namespace Tests.Assets
{
    [TestFixture]
    public class JavaScriptAssetBuilderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Methods_Return_Interface_For_Chaining()
        {
            var builder = new JavaScriptAssetsBuilder();

            Assert.IsInstanceOf(typeof(IJavaScriptAssetsBuilder), builder.Path("test", p => p.ToString()));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Path_Method_Path_Cannot_Be_Rooted()
        {
            var builder = new JavaScriptAssetsBuilder();
            builder.Path("\\test\\", null);
        }
    }
}
