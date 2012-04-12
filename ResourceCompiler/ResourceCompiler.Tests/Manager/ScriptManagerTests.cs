namespace ResourceCompiler.Web.Mvc.Tests
{
    using ResourceCompiler.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class ScriptManagerTests
    {
        [Test]
        public void Can_Get_Default_Group()
        {
            var manager = new ScriptManager(new WebAssetGroupCollection());

            Assert.IsInstanceOf<WebAssetGroup>(manager.DefaultGroup);
        }

        [Test]
        public void Default_Groups_Path_Is_Set_To_MVC_Script_Path_By_Default()
        {
            var manager = new ScriptManager(new WebAssetGroupCollection());

            Assert.AreEqual(DefaultSettings.StyleSheetFilesPath, manager.DefaultGroup.DefaultPath);
        }


        [Test]
        public void Should_Add_Default_Group_To_Collection()
        {
            var collection = new WebAssetGroupCollection();
            var manager = new ScriptManager(collection);

            Assert.AreEqual(1, collection.Count);
            Assert.True(collection.FindGroupByName(DefaultSettings.DefaultGroupName).Name.IsCaseSensitiveEqual(DefaultSettings.DefaultGroupName));
        }

    }
}
