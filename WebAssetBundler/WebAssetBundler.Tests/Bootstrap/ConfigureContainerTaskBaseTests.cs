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
    using System;
    using System.Linq;

    [TestFixture]
    public class ConfigureContainerTaskBaseTests
    {
        private ConfigureContainerTaskBase<BundleImpl> task;
        private Mock<ITypeProvider> typeProvider;

        [SetUp]
        public void Setup()
        {
            task = new ConfigureContainerTaskBaseImpl();
            typeProvider = new Mock<ITypeProvider>();
        }

        [Test]
        public void Should_Get_Pipeline_Modifiers()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();          
            
            task.GetPipelineModifiers(plugin.Object).ToList();

            plugin.Verify(p => p.AddPipelineModifers(It.IsAny<ICollection<IPipelineModifier<BundleImpl>>>()));
        }

        [Test]
        public void Should_Get_Search_Patterns()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();
            
            task.GetSearchPatterns(plugin.Object)
                .ToList<string>();

            plugin.Verify(p => p.AddSearchPatterns(It.IsAny<ICollection<string>>()));
        }

        [Test]
        public void Should_Load_Plugins()
        {
            var container = new TinyIoCContainer();
            container.Register<IPlugin<BundleImpl>, TestPlugin>();

            typeProvider.Setup(p => p.GetImplementationTypes(typeof(IPlugin<BundleImpl>)))
                .Returns(new List<Type>() { typeof(IPlugin<BundleImpl>) });

            var plugins = new List<IPlugin<BundleImpl>>(task.LoadPlugins(container, typeProvider.Object));

            Assert.AreEqual(1, plugins.Count);
            Assert.IsInstanceOf<TestPlugin>(plugins[0]);
            Assert.AreEqual(task.Plugins, plugins);
        }

        [Test]
        public void Should_Dispose_Of_Loaded_Plugins_On_Shut_Down()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();

            task.Plugins = new List<IPlugin<BundleImpl>>()
            {
                plugin.Object
            };

            task.ShutDown();

            plugin.Verify(p => p.Dispose());
        }

        [Test]
        public void Should_Modify_Pipeline()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_Register_Directory_Search()
        {
            var container = new TinyIoCContainer();
            var patterns = new List<string>() { ".test" };

            task.RegisterDirectorySearch<BundleImpl>(container, patterns);

            var dirSearch = container.Resolve<IDirectorySearch>(DirectorySearch.GetDirectorySearchName<BundleImpl>());

            Assert.IsInstanceOf<DirectorySearch>(dirSearch);
        }

        internal class TestPlugin : IPlugin<BundleImpl>
        {

            public void Initialize(TinyIoCContainer container)
            {
                throw new System.NotImplementedException();
            }

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public void AddPipelineModifers(ICollection<IPipelineModifier<BundleImpl>> modifiers)
            {
                throw new NotImplementedException();
            }

            public void AddSearchPatterns(ICollection<string> patterns)
            {
                throw new NotImplementedException();
            }
        }
    }
}
