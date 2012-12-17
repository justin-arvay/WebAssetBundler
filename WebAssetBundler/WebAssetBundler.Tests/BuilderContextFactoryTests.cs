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
    using System.Web.Mvc;

    [TestFixture]
    public class BuilderContextFactoryTests
    {
        private BuilderContextFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new BuilderContextFactory();
        }

        [Test]
        public void Should_Create_Script_Context()
        {
            DefaultSettings.ScriptHost = "192.168.1.1";

            var context = factory.CreateScriptContext();

            Assert.AreEqual(true, context.Combine);
            Assert.AreEqual(true, context.Compress);
            Assert.AreEqual("192.168.1.1", context.Host);
            Assert.AreEqual("~/Scripts", context.DefaultPath);
            Assert.AreEqual(false, context.DebugMode);
        }

        [Test]
        public void Should_Create_Style_Sheet_Context()
        {
            DefaultSettings.StyleSheetHost = "192.168.1.1";

            var context = factory.CreateStyleSheetContext();

            Assert.AreEqual(true, context.Combine);
            Assert.AreEqual(true, context.Compress);
            Assert.AreEqual("192.168.1.1", context.Host);
            Assert.AreEqual("~/Content", context.DefaultPath);
            Assert.AreEqual(false, context.DebugMode);
        }
    }
}
