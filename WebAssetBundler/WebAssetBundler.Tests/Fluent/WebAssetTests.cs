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
    using NUnit.Framework;
    using WebAssetBundler.Web.Mvc;

    [TestFixture]
    public class WebAssetTests
    {

        [Test]
        public void Should_Be_Able_To_Get_Source_Set_By_Constructor()
        {
            var source = "source/file.jpg";
            var webAsset = new WebAsset(source);

            Assert.AreEqual(source, webAsset.Source);
        }

        [Test]
        public void Should_Be_Able_To_Get_Name_Without_Ext()
        {
            var name = "file";
            var webAsset = new WebAsset("path/file.css");

            Assert.AreEqual(name, webAsset.Name);
        }

        [Test]
        public void Should_Be_Able_To_Get_Extention()
        {
            var webAsset = new WebAsset("path/file.css");

            Assert.AreEqual("css", webAsset.Extension);
        }
    }
}
