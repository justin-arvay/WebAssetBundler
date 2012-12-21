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
    using System.Web;

    [TestFixture]
    public class BundlerFactoryTests
    {
        private BundlerFactory factory;
        private Mock<ICacheProvider> cacheProvider;
        private Mock<IBuilderContextFactory> contextFactory;

        [SetUp]
        public void Setup()
        {
            cacheProvider = new Mock<ICacheProvider>();
            contextFactory = new Mock<IBuilderContextFactory>();

            factory = new BundlerFactory(
                cacheProvider.Object,
                contextFactory.Object);

            var module = new WabHttpModule();
            module.Init(null);
            /*
            

            var container = WabHttpModule.Host.Container;

            container.Register<IAssetProvider>((c, p) => (new Mock<IAssetProvider>()).Object);
            container.Register<TinyIoCContainer>();
            

            //stylesheet registers            
            container.Register<IBundlePipeline<StyleSheetBundle>>((c, p) => (new Mock<IBundlePipeline<StyleSheetBundle>>()).Object);
            //container.Register<IBundlesCache<StyleSheetBundle>>((c, p) => (new Mock<IBundlesCache<StyleSheetBundle>>()).Object);
            container.Register<IStyleSheetConfigProvider>((c, p) => (new Mock<IStyleSheetConfigProvider>()).Object);

            //script registers            
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => (new Mock<IBundlePipeline<ScriptBundle>>()).Object);
            container.Register<IBundlesCache<ScriptBundle>>((c, p) => (new Mock<IBundlesCache<ScriptBundle>>()).Object);
            container.Register<IScriptConfigProvider>((c, p) => (new Mock<IScriptConfigProvider>()).Object);*/
        }


        [Test]
        public void Should_Create_Style_Sheet_Builder()
        {
            Assert.IsInstanceOf<StyleSheetBundler>(factory.CreateStyleSheetManagerBuilder());
            contextFactory.Verify(c => c.CreateStyleSheetContext(), Times.Once());
        }

        [Test]
        public void Should_Create_Script_Builder()
        {
            Assert.IsInstanceOf<ScriptBundler>(factory.CreateScriptManagerBuilder());
            contextFactory.Verify(c => c.CreateScriptContext(), Times.Once());
        }
    }
}
