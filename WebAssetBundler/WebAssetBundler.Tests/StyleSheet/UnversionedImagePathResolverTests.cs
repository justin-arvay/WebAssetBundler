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
    public class UnversionedImagePathResolverTests
    {
        private UnversionedImagePathResolver resolver;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext();
            resolver = new UnversionedImagePathResolver();
        }

        [Test]
        public void Should_Filter_Relative_Path()
        {
            var path = "../img/test.jpg";
            var targetPath = "/a/a/a/a";

            var result = resolver.Resolve(path, targetPath, "/Content/file.css");

            Assert.AreEqual("../../../../img/test.jpg", result.NewPath);
            Assert.IsTrue(result.Changed);
        }

        [Test]
        public void Should_Not_Filter_Absolute_Path()
        {
            var path = "/img/test.jpg";
            var targetPath = "/a/a/a/a";

            var result = resolver.Resolve(path, targetPath, "/Content/file.css");

            Assert.AreEqual(null, result.NewPath);
            Assert.IsFalse(result.Changed);
        }

        [Test]
        public void Should_Not_Filter_When_Http_Domain()
        {
            var path = "http://www.google.com/img/test.jpg";
            var targetPath = "/a/a/a/a";

            var result = resolver.Resolve(path, targetPath, "/Content/file.css");

            Assert.AreEqual(null, result.NewPath);
            Assert.IsFalse(result.Changed);
        }

        [Test]
        public void Should_Not_Filter_When_Https_Domain()
        {
            var path = "https://www.google.com/img/test.jpg";
            var targetPath = "/a/a/a/a";

            var result = resolver.Resolve(path, targetPath, "/Content/file.css");

            Assert.AreEqual(null, result.NewPath);
            Assert.IsFalse(result.Changed);
        }
    }
}
