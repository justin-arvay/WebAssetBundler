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

namespace WebAssetBundler.Web.Mvc.Less.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class LessPluginTests
    {
        private LessPlugin plugin;

        [SetUp]
        public void Setup()
        {
            plugin = new LessPlugin();
        }

        [Test]
        public void Should_Initialize()
        {
            var container = new TinyIoCContainer();

            plugin.Initialize(container);

            Assert.IsInstanceOf<LessProcessor>(container.Resolve<LessProcessor>());
        }

        [Test]
        public void Should_Add_Modifiers()
        {
            var modifiers = new List<IPipelineModifier<StyleSheetBundle>>();

            plugin.AddPipelineModifers(modifiers);

            Assert.IsInstanceOf<LessPipelineModifier>(modifiers[0]);
        }

        [Test]
        public void Should_Add_Patterns()
        {
            var patterns = new List<string>();

            plugin.AddSearchPatterns(patterns);

            Assert.AreEqual("*.less", patterns[0]);
        }
    }
}
