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
    public class BundeProviderTests
    {
        private BundleProvider provider;
        private BuilderContext context;
        private Mock<IConfigurationFactory> factory;

        [SetUp]
        public void Setup()
        {
            factory = new Mock<IConfigurationFactory>();
            context = new BuilderContext();
            provider = new BundleProvider(context, factory.Object);
        }

        [Test]
        public void Should_Provide_Style_Sheet_Bundles()
        {
            var configs = new List<StyleSheetBundleConfiguration>();
            configs.Add(new StyleSheetBundleConfigurationImpl());

            factory.Setup(f => f.Create<StyleSheetBundleConfiguration, StyleSheetBundle>(context))
                .Returns(configs);

            var bundles = provider.GetBundles<StyleSheetBundle>();

            Assert.AreEqual(1, bundles.Count);    
        }

        [Test]
        public void Should_Provide_Script_Bundles()
        {
            var configs = new List<ScriptBundleConfiguration>();
            configs.Add(new ScriptBundleConfigurationImpl());

            factory.Setup(f => f.Create<ScriptBundleConfiguration, ScriptBundle>(context))
                .Returns(configs);

            var bundles = provider.GetBundles<ScriptBundle>();

            Assert.AreEqual(1, bundles.Count);
        }
    }
}
