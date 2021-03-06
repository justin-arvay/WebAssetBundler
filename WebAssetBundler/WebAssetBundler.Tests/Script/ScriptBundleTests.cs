﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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
    public class ScriptBundleTests
    {
        private ScriptBundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new ScriptBundle();
        }

        [Test]
        public void Should_Set_Type_When_Instantiated()
        {
            Assert.AreEqual(WebAssetType.Script, bundle.Type);
        }

        [Test]
        public void Should_Set_Extension_When_Instantiated()
        {
            Assert.AreEqual("js", bundle.Extension);
        }

        [Test]
        public void Should_Return_Content_Type()
        {
            Assert.AreEqual("text/javascript", bundle.ContentType);
        }

        [Test]
        public void Should_Return_Asset_Separator()
        {
            Assert.AreEqual(";", bundle.AssetSeparator);
        }
    }
}
