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
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.IO;

    [TestFixture]
    public class HtmlElementTests
    {
        private HtmlElement node;

        [SetUp]
        public void Setup()
        {
            node = new HtmlElement("div");
        }

        [Test]
        public void Should_Set_Name()
        {
            var node = new HtmlElement("test");

            Assert.AreEqual("test", node.Name);
            Assert.False(node.SelfClosing);
        }

        [Test]
        public void Should_Be_Self_Closing()
        {
            var node = new HtmlElement("test", true);

            Assert.True(node.SelfClosing);
        }

        [Test]
        public void Should_Add_Attribute()
        {
            node.AddAttribute("testkey", "testvalue");

            Assert.AreEqual("testvalue", node.GetAttribute("testkey"));
        }

        [Test]
        public void Should_Not_Add_Attribute()
        {
            node.AddAttribute(null, null);
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute(null, "test");
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute("", "");
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute("", "test");
            Assert.AreEqual(0, node.GetAttributes().Count);
        }

        [Test]
        public void Should_Merge_Attributes()
        {
            var attrIn = new Dictionary<string, string>();
            attrIn.Add("test", "testone");
            attrIn.Add("testtwo", "testtwo");

            node.AddAttribute("test", "test");
            node.MergeAttributes(attrIn);

            Assert.AreEqual("testone", node.GetAttribute("test"));
            Assert.AreEqual("testtwo", node.GetAttribute("testtwo"));
        }

        [Test]
        public void Should_Add_Class()
        {
            node.AddClass("css-test");
            node.AddClass("css-testtwo");

            Assert.AreEqual("css-test css-testtwo", node.GetAttribute("class"));
        }

        [Test]
        public void Should_Write_To_Output()
        {
            var writer = new StringWriter();

            node.AddClass("test");
            node.AddAttribute("test", "test");

            node.WriteTo(writer);

            Assert.AreEqual("<div class=\"test\" test=\"test\"></div>", writer.ToString());
        }

        [Test]
        public void Should_Write_Self_Closing_To_Output()
        {
            var writer = new StringWriter();
            var node = new HtmlElement("div", true);

            node.AddClass("test");
            node.AddAttribute("test", "test");

            node.WriteTo(writer);

            Assert.AreEqual("<div class=\"test\" test=\"test\"/>", writer.ToString());
        }

        [Test]
        public void Should_Write_Children_To_Output()
        {
            Assert.Ignore();
        }

        [Test]
        public void Should_Add_And_Write_Valueless_Attribute()
        {
            var writer = new StringWriter();
            var node = new HtmlElement("script", false);

            node.AddValuelessAttribute("async");
            Assert.AreEqual(null, node.GetAttributes()["async"]);

            node.WriteTo(writer);            
            Assert.AreEqual("<script async></script>", writer.ToString());
        }
    }
}
