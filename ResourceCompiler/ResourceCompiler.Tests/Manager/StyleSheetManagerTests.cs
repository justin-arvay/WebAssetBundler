namespace ResourceCompiler.Web.Mvc.Tests
{
    using ResourceCompiler.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class StyleSheetManagerTests
    {
        [Test]
        public void Can_Get_Default_Group()
        {
            var manager = new StyleSheetManager(new WebAssetGroupCollection());

            Assert.IsInstanceOf<WebAssetGroup>(manager.DefaultGroup);
        }

        [Test]
        public void Default_Groups_Path_Is_Set_To_MVC_Style_Sheet_Path_By_Default()
        {
            var manager = new StyleSheetManager(new WebAssetGroupCollection());

            Assert.AreEqual(DefaultSettings.StyleSheetFilesPath, manager.DefaultGroup.DefaultPath);
        }


        [Test]
        public void Should_Add_Default_Group_To_Collection()
        {
            var collection = new WebAssetGroupCollection();
            var manager = new StyleSheetManager(collection);

            Assert.AreEqual(1, collection.Count);
            Assert.True(collection.FindGroupByName(DefaultSettings.DefaultGroupName).Name.IsCaseSensitiveEqual(DefaultSettings.DefaultGroupName));
        }

    }
}
