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

    [TestFixture]
    public class PluginLoaderTests
    {
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
