using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler.Resource.StyleSheet;
using ResourceCompiler.Resource;

namespace Tests.Resource.StyleSheet
{
    [TestFixture]
    public class StyleSheetRegistrarTests
    {
        [Test]
        public void Can_Get_Default_Group()
        {
            var registrar = new StyleSheetRegistrar();

            Assert.IsInstanceOf<ResourceGroup>(registrar.DefaultGroup);
        }

        [Test]
        public void Default_Groups_Path_Is_Set_To_MVC_Style_Sheet_Path_By_Default()
        {
            var registrar = new StyleSheetRegistrar();

            Assert.AreEqual(DefaultSettings.StyleSheetFilesPath, registrar.DefaultGroup.DefaultPath);
        }
    }
}
