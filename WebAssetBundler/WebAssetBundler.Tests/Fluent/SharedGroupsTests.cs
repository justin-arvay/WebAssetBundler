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

    public class SharedGroupsTests
    {

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void Should_Get_Manager()
        {
            Assert.IsInstanceOf<SharedGroupManager>(SharedGroups.Manager);
        }

        [Test]
        public void Should_Get_Script_Context()
        {
            var context = SharedGroups.ScriptContext;

            Assert.IsInstanceOf<BuilderContext>(context);
            Assert.IsInstanceOf<IAssetFactory>(context.AssetFactory);            
        }

        [Test]
        public void Should_Get_Style_Sheet_Context()
        {
            var context = SharedGroups.StyleSheetContext;

            Assert.IsInstanceOf<BuilderContext>(context);
            Assert.IsInstanceOf<IAssetFactory>(context.AssetFactory);            
        }

        [Test]
        public void Should_Build_Script_Group_Collection()
        {
            SharedGroups.Scripts(s => s.ToString());
        }

        [Test]
        public void Should_Build_Style_Sheet_Group_Collection()
        {
            SharedGroups.StyleSheets(s => s.ToString());
        }
    }
}
