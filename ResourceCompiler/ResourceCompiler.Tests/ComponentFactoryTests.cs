namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using ResourceCompiler;

    [TestFixture]
    public class ComponentFactoryTests
    {
        [Test]
        public void Can_Get_Instance_Of_Style_Sheet_Builder()
        {
            var factory = new ComponentFactory(TestHelper.CreateViewContext());

            Assert.IsInstanceOf<StyleSheetRegistrarBuilder>(factory.StyleSheetRegistrar());
        }
    }
}
