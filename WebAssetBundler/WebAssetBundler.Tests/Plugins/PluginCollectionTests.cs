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
        public void Should_Add_Search_Patterns()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_Add_Modifiers()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_Initialize()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_Dispose()
        {
            Assert.Fail();
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
        public void Should_Get_Pipeline_Modifiers()
        {
            var plugin = new Mock<IPlugin<BundleImpl>>();

            task.GetPipelineModifiers(plugin.Object).ToList();

            plugin.Verify(p => p.AddPipelineModifers(It.IsAny<ICollection<IPipelineModifier<BundleImpl>>>()));
        }
    }
}
