using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler.Resource.StyleSheet;

namespace Tests.Resource.StyleSheet
{
    [TestFixture]
    public class StyleSheetRegistrarBuilderTests
    {
        [Test]
        public void Default_Group_Returns_Self_For_Chaining()
        {
            var builder = new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(), TestHelper.CreateViewContext());

            Assert.IsInstanceOf<StyleSheetRegistrarBuilder>(builder.DefaultGroup(g => g.ToString()));
        }

        [Test]
        public void Can_Configure_Default_Group()
        {
            var registrar = new StyleSheetRegistrar();
            var builder = new StyleSheetRegistrarBuilder(registrar, TestHelper.CreateViewContext());

            builder.DefaultGroup(g => g.Add("test/test.js"));

            Assert.AreEqual(1, registrar.DefaultGroup.Resources.Count);
        }
    }
}
