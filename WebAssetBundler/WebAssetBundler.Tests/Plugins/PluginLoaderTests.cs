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
    using System;
    using System.Collections.Generic;
    using TinyIoC;

    [TestFixture]
    public class PluginLoaderTests
    {
        private TinyIoCContainer container;
        private Mock<ITypeProvider> typeProvider;
        private PluginLoader pluginLoader;

        [SetUp]
        public void Setup()
        {
            typeProvider = new Mock<ITypeProvider>();
            container = new TinyIoCContainer();
            pluginLoader = new PluginLoader(container, typeProvider.Object);
        }

        [Test]
        public void Should_Load_Plugins()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();
            container.Register<IPlugin<BundleImpl>>(plugin.Object);

            typeProvider.Setup(p => p.GetImplementationTypes(typeof(IPlugin<BundleImpl>)))
                .Returns(new List<Type>() { typeof(IPlugin<BundleImpl>) });

            var plugins = pluginLoader.LoadPlugins<BundleImpl>();

            Assert.IsTrue(plugins.Contains(plugin.Object));
            plugin.Verify(p => p.Initialize(container));
        }
    }
}
