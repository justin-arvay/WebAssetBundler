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

namespace WebAssetBundler.Web.Mvc.Less.Tests
{
    using NUnit.Framework;
    using Moq;
    using WebAssetBundler.Web.Mvc.Tests;

    [TestFixture]
    public class LessProcessorTests
    {
        private LessProcessor processor;
        private Mock<ICompiler> compiler;

        [SetUp]
        public void Setup()
        {
            compiler = new Mock<ICompiler>();
            processor = new LessProcessor(compiler.Object);
        }
        
        [Test]
        public void Should_Process()
        {
            var asset = new AssetBaseImpl("div { width: 1 + 1 }");

            var bundle = new StyleSheetBundle();
            bundle.Assets.Add(asset);            

            processor.Process(bundle);

            Assert.AreEqual("div {\n  width: 2;\n}\n", bundle.Content);
        }
    }
}
