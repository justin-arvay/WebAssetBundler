using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ResourceCompiler.WebAsset;
using System.Reflection;

namespace Tests.WebAsset
{
    [TestFixture]
    public class DefaultSettingsTests
    {
        [Test]
        public void Can_Set_Style_Sheet_Files_Path()
        {
            DefaultSettings.StyleSheetFilesPath = "~/test/";

            Assert.AreEqual("~/test/", DefaultSettings.StyleSheetFilesPath);

            //re-set the default
            DefaultSettings.StyleSheetFilesPath = "~/Content";
        }

        [Test]
        public void Can_Set_Script_Files_Path()
        {
            DefaultSettings.ScriptFilesPath = "~/test/";

            Assert.AreEqual("~/test/", DefaultSettings.ScriptFilesPath);

            //re-set the default
            DefaultSettings.ScriptFilesPath = "~/Scripts";
        }

        [Test]
        public void Can_Set_Version()
        {
            DefaultSettings.Version = "1.0";

            Assert.AreEqual("1.0", DefaultSettings.Version);

            //re-set the defualt
            DefaultSettings.Version = typeof(DefaultSettings).Assembly.GetName().Version.ToString(3);
        }

        [Test]
        public void Compressed_Should_Be_True_By_Default()
        {
            Assert.True(DefaultSettings.Compressed);
        }

        [Test]
        public void Combined_Should_Be_False_By_Default()
        {
            Assert.False(DefaultSettings.Combined);
        }

        [Test]
        public void Version_Should_Use_Assembly_For_Version_By_Default()
        {
            var version = new AssemblyName(typeof(DefaultSettings).Assembly.FullName).Version.ToString(3);
            Assert.AreEqual(version, DefaultSettings.Version);
        }

        [Test]
        public void ScriptsFilesPath_Should_Use_MVC_Scripts_Folder_By_Default()
        {
            Assert.AreEqual("~/Scripts", DefaultSettings.ScriptFilesPath);
        }

        [Test]
        public void StyleSheetFilesPath_Should_Use_MVC_Content_Folder_By_Default()
        {
            Assert.AreEqual("~/Content", DefaultSettings.StyleSheetFilesPath);
        }

        [Test]
        public void Can_Set_Compressed()
        {
            DefaultSettings.Compressed = false;

            Assert.False(DefaultSettings.Compressed);

            //re-set the default
            DefaultSettings.Compressed = true;
        }

        [Test]
        public void Can_Set_Combined()
        {
            DefaultSettings.Combined = true;

            Assert.True(DefaultSettings.Combined);

            //re-set the default
            DefaultSettings.Combined = false;
        }

        [Test]
        public void Default_Group_Name_Should_Already_Be_Set()
        {
            Assert.AreEqual("Default", DefaultSettings.DefaultGroupName);
        }

        [Test]
        public void Can_Set_Default_Group_Name()
        {
            var previous = DefaultSettings.DefaultGroupName;

            DefaultSettings.DefaultGroupName = "SomeGroup";
            Assert.AreEqual("SomeGroup", DefaultSettings.DefaultGroupName);

            DefaultSettings.DefaultGroupName = previous;
        }
    }
}
