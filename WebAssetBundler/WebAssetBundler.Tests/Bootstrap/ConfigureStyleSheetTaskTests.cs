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
    using System.Collections.Generic;
    using TinyIoC;

    [TestFixture]
    public class ConfigureStyleSheetTaskTests
    {
        private ConfigureStyleSheetsTask task;
        private Mock<IPluginLoader> pluginLoader;

        [SetUp]
        public void Setup()
        {
            pluginLoader = new Mock<IPluginLoader>();
            task = new ConfigureStyleSheetsTask(pluginLoader.Object);
        }

        [Test]
        public void Should_Load_Plugins()
        {
            var container = new TinyIoCContainer();
            var typeProvider = new Mock<ITypeProvider>();

            task.StartUp(container, typeProvider.Object);

            pluginLoader.Verify(p => p.LoadPlugins<StyleSheetBundle>());
        }

        [Test]
        public void Should_Dispose_Of_Loaded_Plugins_On_Shut_Down()
        {
            var plugin = new Mock<IPlugin<StyleSheetBundle>>();

            task.Plugins = new PluginCollection<StyleSheetBundle>()
            {
                plugin.Object
            };

            task.ShutDown();

            plugin.Verify(p => p.Dispose());
        }

        [Test]
        public void Should_Create_Pipeline()
        {
            var container = new TinyIoCContainer();        
            container.Register<IStyleSheetMinifier, MsStyleSheetMinifier>();
            container.Register<ICacheProvider, CacheProvider>();
            container.Register<IBundlesCache<ImageBundle>, BundlesCache<ImageBundle>>();
            container.Register<IUrlGenerator<ImageBundle>, ImageUrlGenerator>();
            container.Register<SettingsContext>(new SettingsContext());
            container.Register<IUrlGenerator<StyleSheetBundle>, BasicUrlGenerator<StyleSheetBundle>>();

            var plugins = new PluginCollection<StyleSheetBundle>();
            var pipeline = task.CreatePipeline<StyleSheetPipeline>(container, plugins);

            Assert.IsInstanceOf<StyleSheetPipeline>(pipeline);            
        }
    }
}
