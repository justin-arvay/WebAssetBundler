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
    using System.IO;

    [TestFixture]
    public class CompilerModifierTests
    {
        private CompilerModifier modifier;
        private Mock<ICompiler> compiler;

        [SetUp]
        public void Setup()
        {
            compiler = new Mock<ICompiler>();
            modifier = new CompilerModifier(compiler.Object);
        }

        [Test]
        public void Should_Transform_Asset_By_Compiling()
        {
            var asset = new AssetBaseImpl("test");

            compiler.Setup(c => c.Compile(It.IsAny<Stream>())).Returns(() => asset.OpenStream());

            var stream = modifier.Modify(asset.OpenStream());            

            compiler.Verify(c => c.Compile(It.IsAny<Stream>()));
            Assert.AreEqual(stream, asset.OpenStream());
        }
    }
}
