// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using ResourceCompiler;
    using Moq;

    [TestFixture]
    public class ComponentFactoryTests
    {
        private Mock<ICacheProvider> cacheProvider;

        [SetUp]
        public void Setup()
        {
            this.cacheProvider = new Mock<ICacheProvider>();
        }

        [Test]
        public void Can_Get_Instance_Of_Style_Sheet_Builder()
        {
            var factory = new ComponentFactory(TestHelper.CreateViewContext(), cacheProvider.Object);

            Assert.IsInstanceOf<StyleSheetManagerBuilder>(factory.StyleSheetManager());
        }

        [Test]
        public void Can_Get_Instance_Of_Script_Builder()
        {
            var factory = new ComponentFactory(TestHelper.CreateViewContext(), cacheProvider.Object);

            Assert.IsInstanceOf<ScriptManagerBuilder>(factory.ScriptManager());
        }
    }
}
