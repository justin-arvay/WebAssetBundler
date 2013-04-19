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
    public class ExpandPathProcessorTests
    {
        private ExpandPathProcessor processor;
        private StyleSheetBundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new StyleSheetBundle();
            bundle.Assets.Add(new AssetBaseImpl());
            processor = new ExpandPathProcessor();
        }

        [Test]
        public void Should_Filter_Relative_Path()
        {
            bundle.Assets[0].Content = "url(\"../img/test.jpg\");";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);
        

            Assert.AreEqual("url(\"../../../../img/test.jpg\");", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Filter_Absolute_Path()
        {
            bundle.Assets[0].Content = "url(\"/img/test.jpg\");";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url(\"/img/test.jpg\");", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Filter_Full_Path()
        {
            bundle.Assets[0].Content = "url(\"http://www.google.com/img/test.jpg\");";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url(\"http://www.google.com/img/test.jpg\");", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Url_With_Double_Quotes()
        {
            bundle.Assets[0].Content = "url(\"/img/test.jpg\");";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url(\"/img/test.jpg\");", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Url_With_Single_Quotes()
        {
            bundle.Assets[0].Content = "url('/img/test.jpg');";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url('/img/test.jpg');", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Url_With_No_Quotes()
        {
          
            bundle.Assets[0].Content = "url(/img/test.jpg);";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("url(/img/test.jpg);", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Src_With_Double_Quotes()
        {
            bundle.Assets[0].Content = "src=\"/img/test.jpg\"";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("src=\"/img/test.jpg\"", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Src_With_Single_Quotes()
        {
            bundle.Assets[0].Content = "src='/img/test.jpg'";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("src='/img/test.jpg'", bundle.Assets[0].Content);
        }

        [Test]
        public void Should_Match_Src_With_No_Quotes()
        {
            bundle.Assets[0].Content = "src=/img/test.jpg";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);

            Assert.AreEqual("src=/img/test.jpg", bundle.Assets[0].Content);
        }            
    }
}
