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
    public class ManagerBuilderFactoryTests
    {
        private ManagerBuilderFactory factory;
        private Mock<ICacheProvider> cacheProvider;
        private Mock<IBuilderContextFactory> contextFactory;

        [SetUp]
        public void Setup()
        {
            cacheProvider = new Mock<ICacheProvider>();
            contextFactory = new Mock<IBuilderContextFactory>();

            factory = new ManagerBuilderFactory(
                TestHelper.CreateViewContext(),
                cacheProvider.Object,
                contextFactory.Object);
        }

        [Test]
        public void Should_Create_Style_Sheet_Builder()
        {
            Assert.IsInstanceOf<StyleSheetManagerBuilder>(factory.CreateStyleSheetManagerBuilder());
            contextFactory.Verify(c => c.CreateStyleSheetContext(), Times.Once());
        }

        [Test]
        public void Should_Create_Script_Builder()
        {
            Assert.IsInstanceOf<ScriptManagerBuilder>(factory.CreateScriptManagerBuilder());
            contextFactory.Verify(c => c.CreateScriptContext(), Times.Once());
        }
    }
}
