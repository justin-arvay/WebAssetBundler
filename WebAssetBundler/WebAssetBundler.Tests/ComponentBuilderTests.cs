// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using WebAssetBundler;
    using Moq;
    using System.Web;
    using System.Web.Mvc;

    [TestFixture]
    public class ComponentBuilderTests
    {
        /*
        private Mock<ICacheProvider> cacheProvider;

        private StyleSheetBundler CreateStyleSheetManagerBuilder()
        {
            var builderContext = new BundleContext();
            var tagWriter = new Mock<ITagWriter<StyleSheetBundle>>();
            var bundleProvider = new Mock<IBundleProvider<StyleSheetBundle>>();

            return new StyleSheetBundler(
                bundleProvider.Object,
                tagWriter.Object,
                builderContext);
        }

        private ScriptBundler CreateScriptManagerBuilder()
        {
            var builderContext = new BundleContext();
            var tagWriter = new Mock<ITagWriter<ScriptBundle>>();
            var bundleProvider = new Mock<IBundleProvider<ScriptBundle>>();

            return new ScriptBundler(
                bundleProvider.Object,
                tagWriter.Object,
                builderContext);
        }

        [SetUp]
        public void Setup()
        {
            this.cacheProvider = new Mock<ICacheProvider>();
        }

        [Test]
        public void Can_Get_Instance_Of_Style_Sheet_Builder()
        {
            var factory = new ComponentBuilder(CreateStyleSheetManagerBuilder(), CreateScriptManagerBuilder());

            Assert.IsInstanceOf<StyleSheetBundler>(factory.StyleSheetManager());
        }

        [Test]
        public void Should_Be_Same_Style_Sheet_Instance()
        {
            var factory = new ComponentBuilder(CreateStyleSheetManagerBuilder(), CreateScriptManagerBuilder());

            var builderOne = factory.StyleSheetManager();
            var builderTwo = factory.StyleSheetManager();

            Assert.AreSame(builderOne, builderTwo);
        }

        [Test]
        public void Can_Get_Instance_Of_Script_Builder()
        {
            var factory = new ComponentBuilder(CreateStyleSheetManagerBuilder(), CreateScriptManagerBuilder());

            Assert.IsInstanceOf<ScriptBundler>(factory.ScriptManager());
        }

        [Test]
        public void Should_Be_Same_Script_Instance()
        {
            var factory = new ComponentBuilder(CreateStyleSheetManagerBuilder(), CreateScriptManagerBuilder());

            var builderOne = factory.ScriptManager();
            var builderTwo = factory.ScriptManager();

            Assert.AreSame(builderOne, builderTwo);
        }
         * */
    }
}
