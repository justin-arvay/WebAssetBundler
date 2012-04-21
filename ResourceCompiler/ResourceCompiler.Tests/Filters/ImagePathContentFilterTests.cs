// ResourceCompiler - Compiles web assets so you dont have to.
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
