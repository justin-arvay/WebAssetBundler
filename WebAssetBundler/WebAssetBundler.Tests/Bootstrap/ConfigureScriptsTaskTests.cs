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

    [TestFixture]
    public class ConfigureScriptsTaskTests
    {
        private ConfigureScriptsTask task;
        private Mock<IPluginLoader> pluginLoader;

        [SetUp]
        public void Setup()
        {
            pluginLoader = new Mock<IPluginLoader>();
            task = new ConfigureScriptsTask(pluginLoader.Object);
        }

        [Test]
        public void Should_Load_Plugins()
        {
            var container = new TinyIoCContainer();
            var typeProvider = new Mock<ITypeProvider>();

            task.StartUp(container, typeProvider.Object);

            pluginLoader.Verify(p => p.LoadPlugins<ScriptBundle>());
        }

        [Test]
        public void Should_Dispose_Of_Loaded_Plugins_On_Shut_Down()
        {
            var plugin = new Mock<IPlugin<ScriptBundle>>();

            task.Plugins = new PluginCollection<ScriptBundle>()
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
            var plugins = new PluginCollection<ScriptBundle>();

            container.Register<IScriptMinifier, MsScriptMinifier>();
            container.Register<IUrlGenerator<ScriptBundle>, BasicUrlGenerator<ScriptBundle>>();

            var pipeline = task.CreateScriptPipeline(container, plugins);

            Assert.IsInstanceOf<ScriptPipeline>(pipeline);            
        }
    }
}
