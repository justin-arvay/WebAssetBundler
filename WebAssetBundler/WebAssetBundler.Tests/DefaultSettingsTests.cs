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
    using Moq;
    using TinyIoC;

    [TestFixture]
    public class DefaultSettingsTests
    {

        [Test]
        public void Should_Be_Default_Logger()
        {
            var logger = DefaultSettings.Logger;

            Assert.IsInstanceOf<DoNothingLogger>(logger);
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
    }
}
