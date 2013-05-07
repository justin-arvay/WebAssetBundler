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

    [TestFixture]
    public class MinifyModifierTests
    {
        private MinifyModifier modifier;
        private Mock<IMinifier> minifier;

        [SetUp]
        public void Setup()
        {
            minifier = new Mock<IMinifier>();
            modifier = new MinifyModifier(minifier.Object);
        }

        [Test]
        public void Should_Minify()
        {
            var asset = new AssetBaseImpl("test");

            minifier.Setup(m => m.Minify("test")).Returns("test");

            var returnStream = modifier.Modify(asset.Content);

            minifier.Verify(m => m.Minify("test"));
            Assert.AreEqual("test", returnStream.ReadToEnd());
        }
    }
}
