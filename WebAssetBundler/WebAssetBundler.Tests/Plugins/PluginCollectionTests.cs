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
    public class PluginCollectionTests
    {
        private PluginCollection<BundleImpl> collection;

        [SetUp]
        public void Setup()
        {
            collection = new PluginCollection<BundleImpl>();
        }

        [Test]
        public void Should_Initialize()
        {
            var container = new TinyIoCContainer();
            var plugin = new Mock<IPlugin<BundleImpl>>();            

            collection.Add(plugin.Object);
            collection.Initialize(container);

            plugin.Verify(p => p.Initialize(container));
        }

        [Test]
        public void Should_Dispose()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();

            collection.Add(plugin.Object);
            collection.Dispose();

            plugin.Verify(p => p.Dispose());
        }

        [Test]
        public void Should_Modify_Search_Patterns()
        {
            collection.Add(new TestPlugin());

            var patterns = collection.ModifySearchPatterns();

            Assert.IsTrue(patterns.Contains(".tst"));
        }


        [Test]
        public void Should_Modify_Pipeline()
        {
            var plugin = new TestPlugin();

            collection.Add(plugin);

            var modifiers = collection.ModifyPipeline();

            Assert.IsTrue(modifiers.Contains(plugin.Modifier));
        }

        internal class TestPlugin : IPlugin<BundleImpl>
        {
            public void Initialize(TinyIoCContainer container)
            {
                throw new System.NotImplementedException();
            }

            public void ModifyPipeline(IBundlePipeline<BundleImpl> pipeline)
            {

            }

            public void ModifySearchPatterns(System.Collections.Generic.ICollection<string> patterns)
            {
                patterns.Add(".tst");
            }

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
