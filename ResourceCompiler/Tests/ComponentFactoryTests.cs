using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler;
using ResourceCompiler.Resource.StyleSheet;

namespace Tests
{
    [TestFixture]
    public class ComponentFactoryTests
    {
        [Test]
        public void Can_Get_Instance_Of_Style_Sheet_Builder()
        {
            var factory = new ComponentFactory();

            Assert.IsInstanceOf<StyleSheetRegistrarBuilder>(factory.StyleSheetRegistrar());
        }

        [Test]
        public void Always_Return_Same_Instance_Of_Style_Sheet_Builder()
        {
            var factory = new ComponentFactory();

            var builderOne = factory.StyleSheetRegistrar();
            var builderTwo = factory.StyleSheetRegistrar();


            Assert.AreSame(builderOne, builderTwo);
        }
    }
}
