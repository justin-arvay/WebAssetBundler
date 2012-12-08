// ResourceCompiler - Compiles web assets so you dont have to.
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
    public class ScriptCompressProcessorTests
    {
        private ScriptCompressProcessor processor;
        private Mock<IScriptCompressor> compressor;
        private ScriptBundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new ScriptBundle();
            compressor = new Mock<IScriptCompressor>();
            processor = new ScriptCompressProcessor(compressor.Object);
        }

        [Test]
        public void Should_Compress_Assets()
        {
            var asset = new AssetBaseImpl();
            asset.Content = "var value = 1;";
            bundle.Assets.Add(asset);

            processor.Process(bundle);

            compressor.Verify(c => c.Compress("var value = 1;"), Times.Once());
        }
    }
}
