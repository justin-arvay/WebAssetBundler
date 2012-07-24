// Web Asset Bundler - Bundles web assets so you dont have to.
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
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class BuilderContextTests
    {
        private BuilderContext context;

        [SetUp]
        public void Setup()
        {
            context = new BuilderContext(WebAssetType.None);
        }
        
        [Test]
        public void Should_Set_Type_Enum()
        {
            Assert.AreEqual(WebAssetType.None, context.TypeEnum);        }

        [Test]
        public void Should_Get_Compress_From_Settings()
        {
            Assert.AreEqual(DefaultSettings.Compressed, context.Compress);
        }

        [Test]
        public void Should_Get_Combine_From_Settings()
        {
            Assert.AreEqual(DefaultSettings.Combined, context.Combine);
        }

        [Test]
        public void Should_Get_Style_Sheet_Default_Path_From_Settings()
        {
            context = new BuilderContext(WebAssetType.StyleSheet);

            Assert.AreEqual(DefaultSettings.StyleSheetFilesPath, context.DefaultPath);
        }

        [Test]
        public void Should_Get_Script_Default_Path_From_Settings()
        {
            context = new BuilderContext(WebAssetType.Script);

            Assert.AreEqual(DefaultSettings.ScriptFilesPath, context.DefaultPath);
        }
    }
}
