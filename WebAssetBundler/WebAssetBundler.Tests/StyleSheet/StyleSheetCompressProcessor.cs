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
    public class StyleSheetCompressProcessorTests
    {
        private StyleSheetCompressProcessor processor;
        private Mock<IStyleSheetCompressor> compressor;
        private StyleSheetBundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new StyleSheetBundle();
            compressor = new Mock<IStyleSheetCompressor>();
            processor = new StyleSheetCompressProcessor(compressor.Object);
        }

        [Test]
        public void Should_Compress_Assets()
        {
            var asset = new AssetBaseImpl();
            asset.Content = "#div { color: #123; }";

            bundle.Assets.Add(asset);
            bundle.Compress = true;

            processor.Process(bundle);

            compressor.Verify(c => c.Compress("#div { color: #123; }"), Times.Once());
        }

        [Test]
        public void Should_Not_Compress_Asset()
        {
            var asset = new AssetBaseImpl();
            asset.Content = "#div { color: #123; }";

            bundle.Assets.Add(asset);
            bundle.Compress = false;

            processor.Process(bundle);

            compressor.Verify(c => c.Compress("#div { color: #123; }"), Times.Never());
        }
    }
}
