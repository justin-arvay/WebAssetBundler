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

            node.AddAttribute("test", null);
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute("", "");
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute("", "test");
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute("test", "");
            Assert.AreEqual(0, node.GetAttributes().Count);

            node.AddAttribute("", null);
            Assert.AreEqual(0, node.GetAttributes().Count);
        }

        [Test]
        public void Should_Merge_Attributes()
        {
        }

        [Test]
        public void Should_Add_Class()
        {
        }

        [Test]
        public void Should_Write_To_Output()
        {
        }

        [Test]
        public void Should_Write_Self_Closing_To_Output()
        {
        }
    }
}
