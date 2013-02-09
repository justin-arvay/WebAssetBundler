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
    public class ScriptMinifyProcessorTests
    {
        private ScriptMinifyProcessor processor;
        private Mock<IScriptMinifier> compressor;
        private ScriptBundle bundle;

        [SetUp]
        public void Setup()
        {
            Func<string> minifyIdentifier = () => "min";
            Func<bool> debugMode = () => false;

            bundle = new ScriptBundle();
            compressor = new Mock<IScriptMinifier>();
            processor = new ScriptMinifyProcessor(minifyIdentifier, debugMode, compressor.Object);
        }

        [Test]
        public void Should_Compress_Assets()
        {
            var asset = new AssetBaseImpl();
            asset.Content = "var value = 1;";
            asset.Source = "~/file.js";

            bundle.Assets.Add(asset);
            bundle.Minify = true;

            processor.Process(bundle);

            compressor.Verify(c => c.Minify("var value = 1;"), Times.Once());
        }

        [Test]
        public void Should_Not_Compress_Assets()
        {
            var asset = new AssetBaseImpl();
            asset.Content = "var value = 1;";

            bundle.Assets.Add(asset);
            bundle.Minify = false;

            processor.Process(bundle);

            compressor.Verify(c => c.Minify("var value = 1;"), Times.Never());
        }

        [Test]
        public void Should_Not_Compress_Asset_When_Already_Minified()
        {
            var asset = new AssetBaseImpl();
            asset.Source = "~/file.min.js";
            asset.Content = "var value = 1;";

            bundle.Assets.Add(asset);
            bundle.Minify = true;

            processor.Process(bundle);

            compressor.Verify(c => c.Minify("var value = 1;"), Times.Never());
        }

        [Test]
        public void Should_Not_Compress_Assets_When_Debug_Mode()
        {
            processor = new ScriptMinifyProcessor(() => ".min", () => true, compressor.Object);

            var asset = new AssetBaseImpl();
            asset.Content = "#div { color: #123; }";
            asset.Source = "~/file.js";

            bundle.Assets.Add(asset);
            bundle.Minify = true;

            processor.Process(bundle);

            compressor.Verify(c => c.Minify("var value = 1;"), Times.Never());
        }
    }
}
