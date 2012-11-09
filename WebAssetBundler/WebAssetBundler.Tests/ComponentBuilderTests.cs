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
        private Mock<ICacheProvider> cacheProvider;

        private StyleSheetManagerBuilder CreateStyleSheetManagerBuilder()
        {
            var builderContext = new BuilderContext();
            var tagWriter = new Mock<ITagWriter>();
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new BundleCollection();            
            var resolverFactory = new Mock<IWebAssetResolverFactory>();
            var collectionResolver = new WebAssetBundleCollectionResolver(resolverFactory.Object);
            var merger = new Mock<IWebAssetMerger>();

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(collection),
                TestHelper.CreateViewContext(),
                collectionResolver,
                tagWriter.Object,
                merger.Object,
                builderContext);
        }

        private ScriptManagerBuilder CreateScriptManagerBuilder()
        {
            var builderContext = new BuilderContext();
            var tagWriter = new Mock<ITagWriter>();
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new BundleCollection();
            var merger = new Mock<IWebAssetMerger>();
            var resolverFactory = new Mock<IWebAssetResolverFactory>();
            var collectionResolver = new WebAssetBundleCollectionResolver(resolverFactory.Object);

            return new ScriptManagerBuilder(
                new ScriptManager(collection),
                TestHelper.CreateViewContext(),
                collectionResolver,
                tagWriter.Object,
                merger.Object,
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

            Assert.IsInstanceOf<StyleSheetManagerBuilder>(factory.StyleSheetManager());
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

            Assert.IsInstanceOf<ScriptManagerBuilder>(factory.ScriptManager());
        }

        [Test]
        public void Should_Be_Same_Script_Instance()
        {
            var factory = new ComponentBuilder(CreateStyleSheetManagerBuilder(), CreateScriptManagerBuilder());

            var builderOne = factory.ScriptManager();
            var builderTwo = factory.ScriptManager();

            Assert.AreSame(builderOne, builderTwo);
        }
    }
}
