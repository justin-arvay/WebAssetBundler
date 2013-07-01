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
    using TinyIoC;

    [TestFixture]
    public class BundleBaseTaskTests
    {
        private BundleBaseTaskImpl task;

        [SetUp]
        public void Setup()
        {
            task = new BundleBaseTaskImpl();
        }

        [Test]
        public void Should_Dispose_Of_Loaded_Plugins_On_Shut_Down()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();

            task.Plugins = new PluginCollection<BundleImpl>()
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
            container.Register<ILogger>(new DoNothingLogger());

            var plugins = new PluginCollection<BundleImpl>();
            var pipeline = task.CreatePipeline<BundlePipelineImpl>(container, plugins);

            Assert.IsInstanceOf<BundlePipelineImpl>(pipeline);
        }
    }
}
