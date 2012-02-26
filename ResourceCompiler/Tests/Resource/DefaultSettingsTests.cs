using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ResourceCompiler.Resource;

namespace Tests.Resource
{
    [TestFixture]
    public class DefaultSettingsTests
    {
        [Test]
        public void Can_Set_Style_Sheet_Files_Path()
        {
            DefaultSettings.StyleSheetFilesPath = "~/test/";

            Assert.AreEqual("~/test/", DefaultSettings.StyleSheetFilesPath);
        }

        [Test]
        public void Can_Set_Script_Files_Path()
        {
            DefaultSettings.ScriptFilesPath = "~/test/";

            Assert.AreEqual("~/test/", DefaultSettings.ScriptFilesPath);
        }

        [Test]
        public void Can_Set_Version()
        {
            DefaultSettings.Version = "1.0";

            Assert.AreEqual("1.0", DefaultSettings.Version);
        }
    }
}
