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
            var plugin = new Mock<IPlugin<BundleImpl>>();
            var patterns = new List<string>();

            collection.Add(plugin.Object);
            collection.ModifySearchPatterns(patterns);

            plugin.Verify(p => p.ModifySearchPatterns(patterns));
        }


        [Test]
        public void Should_Modify_Pipeline()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();
            var container = new TinyIoCContainer();
            var pipeline = new TestStyleSheetPipeline(container);

            collection.Add(plugin.Object);
            collection.ModifyPipeline(pipeline);

            plugin.Verify(p => p.ModifyPipeline(pipeline));
        }

        internal class TestStyleSheetPipeline : BundlePipeline<BundleImpl>
        {
            public TestStyleSheetPipeline(TinyIoCContainer container)
                : base(container)
            {
            }
        }
    }
}
