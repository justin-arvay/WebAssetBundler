// Web Asset Bundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using System.Web;

    [TestFixture]
    public class ImagePathProcessorTests
    {
        private ImagePathProcessor processor;
        private Mock<IUrlGenerator<StyleSheetBundle>> urlGenerator;
        private BundleContext context;
        private StyleSheetBundle bundle;

        [SetUp]
        public void Setup()
        {
            urlGenerator = new Mock<IUrlGenerator<StyleSheetBundle>>();
            context = new BundleContext();

            bundle = new StyleSheetBundle();
            bundle.Assets.Add(new AssetBaseImpl());

            processor = new ImagePathProcessor(urlGenerator.Object, context);

            urlGenerator.Setup(u => u.Generate("a", "a", "", context)).Returns("/wab.axd/css/a/a");
        }

        [Test]
        public void Should_Filter_Relative_Path()
        {
            bundle.Assets[0].Content = "url(\"../img/test.jpg\");";

            processor.Process(bundle);
        

            Assert.AreEqual("url(\"../../../../img/test.jpg\");", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Filter_Absolute_Path()
        {
            bundle.Assets[0].Content = "url(\"/img/test.jpg\");";

            processor.Process(bundle);


            Assert.AreEqual("url(\"/img/test.jpg\");", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Filter_Full_Path()
        {
            bundle.Assets[0].Content = "url(\"http://www.google.com/img/test.jpg\");";

            processor.Process(bundle);


            Assert.AreEqual("url(\"http://www.google.com/img/test.jpg\");", bundle.Assets[0].Content);
        }
        /*
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

        [Test]
        public void Should_Process_Each_Asset()
        {
        }
         */
    }
}
