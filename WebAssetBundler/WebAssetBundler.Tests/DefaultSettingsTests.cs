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
            Assert.IsInstanceOf<MsScriptMinifier>(DefaultSettings.ScriptMinifier);
        }

        [Test]
        public void Can_Set_Script_Compressor()
        {
            var previous = DefaultSettings.ScriptMinifier;

            DefaultSettings.ScriptMinifier = new YuiScriptMinifier();
            Assert.IsInstanceOf<YuiScriptMinifier>(DefaultSettings.ScriptMinifier);

            DefaultSettings.ScriptMinifier = previous;
        }

        [Test]
        public void Should_Use_Ms_Style_Sheet_Compressor_By_Defaut()
        {
            Assert.IsInstanceOf<MsStyleSheetMinifier>(DefaultSettings.StyleSheetMinifier);
        }

        [Test]
        public void Can_Set_Style_Sheet_Compressor()
        {
            var previous = DefaultSettings.StyleSheetMinifier;

            DefaultSettings.StyleSheetMinifier = new YuiStyleSheetMinifier();
            Assert.IsInstanceOf<YuiStyleSheetMinifier>(DefaultSettings.StyleSheetMinifier);

            DefaultSettings.StyleSheetMinifier = previous;
        }

        [Test]
        public void Should_Be_Default_Style_Sheet_Config_Factory()
        {
            Assert.IsInstanceOf<DefaultBundleConfigurationFactory<StyleSheetBundle>>(DefaultSettings.StyleSheetBundleConfigurationFactory);
        }

        [Test]
        public void Should_Be_Default_Script_Config_Factory()
        {
            Assert.IsInstanceOf<DefaultBundleConfigurationFactory<ScriptBundle>>(DefaultSettings.ScriptBundleConfigurationFactory);
        }

        [Test]
        public void Should_Be_Dot_Min_By_Default()
        {
            Assert.AreEqual(".min", DefaultSettings.MinifyIdentifier);
        }
    }
}
