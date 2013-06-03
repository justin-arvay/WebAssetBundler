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
    public class BundlerTests
    {
        [Test]
        public void Should_Create_StyleSheet_Bundler()
        {
            StyleSheetBundler bundler = Bundler.StyleSheets;
            StyleSheetBundler bundlerTwo = Bundler.StyleSheets;

            Assert.IsInstanceOf<StyleSheetBundler>(bundler);
            Assert.AreSame(bundler, bundlerTwo);
            Assert.IsInstanceOf<BundlerState>(bundler.State);
            Assert.AreSame(bundler.State, bundlerTwo.State);
        }

        [Test]
        public void Should_Create_Script_Bundler()
        {
            ScriptBundler bundler = Bundler.Scripts;
            ScriptBundler bundlerTwo = Bundler.Scripts;

            Assert.IsInstanceOf<ScriptBundler>(bundler);
            Assert.AreSame(bundler, bundlerTwo);
            Assert.IsInstanceOf<BundlerState>(bundler.State);
            Assert.AreSame(bundler.State, bundlerTwo.State);
        }

        [Test]
        public void Should_Create_Image_Bundler()
        {
            ImageBundler bundler = Bundler.Images;
            ImageBundler bundlerTwo = Bundler.Images;

            Assert.IsInstanceOf<ImageBundler>(bundler);
            Assert.AreSame(bundler, bundlerTwo);
        }
    }
}
