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
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class ComponentBuilderTests
    {
        private ComponentBuilder<BundleImpl> builder;
        private BundleImpl bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
            builder = new ComponentBuilderImpl(bundle);
        }

        [Test]
        public void Should_Add_Class()
        {
            builder.AddClass("test");

            Assert.AreEqual("test", builder.Bundle.Attributes["class"]);
        }

        [Test]
        public void Should_Add_Attribute()
        {
            builder.AddAttribute("test", "testvalue");

            Assert.AreEqual("testvalue", builder.Bundle.Attributes["test"]);
        }

        [Test]
        public void Should_Get_Bundle()
        {
            Assert.AreSame(bundle, builder.Bundle);
        }
    }
}
