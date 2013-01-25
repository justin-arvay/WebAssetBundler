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
    using System;

    [TestFixture]
    public class ScriptUrlGeneratorTests
    {
        private ScriptUrlGenerator generator;
        private Func<bool> debugMode;

        [SetUp]
        public void Setup()
        {
            generator = new ScriptUrlGenerator(debugMode);
        }

        [Test]
        public void Should_Generate_Url()
        {
            var url = generator.Generate("test", "abc", "http://www.test.com");

            Assert.AreEqual("http://www.test.com/wab.axd/js/abc/test", url);
        }

        [Test]
        public void Should_Generate_Url_Without_Host()
        {
            var url = generator.Generate("test", "abc", null);

            Assert.AreEqual("/wab.axd/js/abc/test", url);
        }

        [Test]
        public void Should_Generate_Cache_Breaker_Url()
        {
            var url = generator.Generate("test", "abc", null);

            Assert.AreEqual("/wab.axd/js/abc" + DateTime.Now.ToString("MMddyyHmmss") + "/test", url);
        }
    }
}
