// WebAssetBundler - Bundles web assets so you dont have to.
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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using WebAssetBundler.Web.Mvc;
    using System.Reflection;

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
        public void Should_Use_Ms_Script_Compressor_By_Default()
        {
            Assert.IsInstanceOf<MsScriptCompressor>(DefaultSettings.ScriptCompressor);
        }

        [Test]
        public void Can_Set_Script_Compressor()
        {
            var previous = DefaultSettings.ScriptCompressor;

            DefaultSettings.ScriptCompressor = new YuiScriptCompressor();
            Assert.IsInstanceOf<YuiScriptCompressor>(DefaultSettings.ScriptCompressor);

            DefaultSettings.ScriptCompressor = previous;
        }

        [Test]
        public void Should_Use_Ms_Style_Sheet_Compressor_By_Defaut()
        {
            Assert.IsInstanceOf<MsStyleSheetCompressor>(DefaultSettings.StyleSheetCompressor);
        }

        [Test]
        public void Can_Set_Style_Sheet_Compressor()
        {
            var previous = DefaultSettings.StyleSheetCompressor;

            DefaultSettings.StyleSheetCompressor = new YuiStyleSheetCompressor();
            Assert.IsInstanceOf<YuiStyleSheetCompressor>(DefaultSettings.StyleSheetCompressor);

            DefaultSettings.StyleSheetCompressor = previous;
        }

        [Test]
        public void Should_Be_Default_Style_Sheet_Config_Provider()
        {
            Assert.IsInstanceOf<DefaultStyleSheetConfigProvider>(DefaultSettings.StyleSheetConfigProvider);
        }

        [Test]
        public void Should_Be_Default_Script_Config_Provider()
        {
            Assert.IsInstanceOf<DefaultScriptConfigProvider>(DefaultSettings.ScriptConfigProvider);
        }

        [Test]
        public void Should_Be_Dot_Min_By_Default()
        {
            Assert.AreEqual(".min", DefaultSettings.ScriptMinifyIdentifier);
            Assert.AreEqual(".min", DefaultSettings.StyleSheetMinifyIdentifier);
        }
    }
}
