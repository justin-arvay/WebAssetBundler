using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler.Resource.StyleSheet;


namespace Tests.Resource.StyleSheet
{
    using ResourceCompiler.Resource;
    using ResourceCompiler.Extensions;

    [TestFixture]
    public class StyleSheetRegistrarTests
    {
        [Test]
        public void Can_Get_Default_Group()
        {
            var registrar = new StyleSheetRegistrar(new ResourceGroupCollection());

            Assert.IsInstanceOf<ResourceGroup>(registrar.DefaultGroup);
        }

        [Test]
        public void Default_Groups_Path_Is_Set_To_MVC_Style_Sheet_Path_By_Default()
        {
            var registrar = new StyleSheetRegistrar(new ResourceGroupCollection());

            Assert.AreEqual(DefaultSettings.StyleSheetFilesPath, registrar.DefaultGroup.DefaultPath);
        }


        [Test]
        public void Should_Add_Default_Group_To_Collection()
        {
            var collection = new ResourceGroupCollection();
            var registrar = new StyleSheetRegistrar(collection);

            Assert.AreEqual(1, collection.Count);
            Assert.True(collection.FindGroupByName(DefaultSettings.DefaultGroupName).Name.IsCaseSensitiveEqual(DefaultSettings.DefaultGroupName));
        }

    }
}
