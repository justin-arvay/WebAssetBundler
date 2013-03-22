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

    [TestFixture]
    public class LoadPluginsTaskTests
    {
        private Mock<ITypeProvider> typeProvider;
        private LoadPluginsTask task;
        private Mock<TinyIoCContainer> container;

        [SetUp]
        public void Setup()
        {
            container = new Mock<TinyIoCContainer>();
            typeProvider = new Mock<ITypeProvider>();
            task = new LoadPluginsTask();
        }

        [Test]
        public void Should_Load_Plugins()
        {
            var scriptPlugin = new TestScriptPlugin();
            var styleSheetPlugin = new TestStyleSheetPlugin();

            var scriptBundleTypes = new List<Type>() 
            {
                scriptPlugin.GetType()
            };

            var styleSheetBundleTypes = new List<Type>() 
            {
                styleSheetPlugin.GetType()
            };

            typeProvider.Setup(p => p.GetImplementationTypes(typeof(IPluginConfiguration<ScriptBundle>)))
                .Returns(scriptBundleTypes);

            typeProvider.Setup(p => p.GetImplementationTypes(typeof(IPluginConfiguration<StyleSheetBundle>)))
                .Returns(styleSheetBundleTypes);

            container.Setup(c => c.Resolve(typeof(TestScriptPlugin)))
                .Returns(scriptPlugin);

            container.Setup(c => c.Resolve(typeof(TestStyleSheetPlugin)))
                .Returns(styleSheetPlugin);

            Assert.AreEqual(1, scriptPlugin.ConfigureCount);
            Assert.AreEqual(1, styleSheetPlugin.ConfigureCount);
        }

        public class TestScriptPlugin : IPluginConfiguration<StyleSheetBundle>
        {
            public int ConfigureCount { get; set; }

            public void Configure(TinyIoCContainer container)
            {
                ConfigureCount++;
            }

            public void ConfigurePipelineModifiers(ICollection<IPipelineModifier<StyleSheetBundle>> modifiers)
            {

            }

            public void ConfigurePatternModifiers(ICollection<string> patterns)
            {

            }
        }

        public class TestStyleSheetPlugin : IPluginConfiguration<StyleSheetBundle>
        {
            public int ConfigureCount { get; set; }

            public void Configure(TinyIoCContainer container)
            {
                ConfigureCount++;
            }

            public void ConfigurePipelineModifiers(ICollection<IPipelineModifier<StyleSheetBundle>> modifiers)
            {

            }

            public void ConfigurePatternModifiers(ICollection<string> patterns)
            {

            }
        }
    }
}
