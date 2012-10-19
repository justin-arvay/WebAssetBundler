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

    [TestFixture]
    public class WebAssetMergerResultTest
    {
        [Test]
        public void Should_Set_Version_With_Constructor()
        {
            var result = new MergerResult("", "1.1", "", WebAssetType.None);

            Assert.AreSame(result.Version, "1.1");
        }

        [Test]
        public void Should_Set_Content_With_Constructor()
        {
            var content = "some content";
            var result = new MergerResult("", "", content, WebAssetType.None);

            Assert.AreSame(result.Content, content);
        }

        [Test]
        public void Should_Set_Name_With_Constructor()
        {
            var result = new MergerResult("name", "", "", WebAssetType.None);

            Assert.AreSame(result.Name, "name");
        }

        [Test]
        public void Should_Return_Correct_Content_Type()
        {
            var result = new MergerResult("", "", "", WebAssetType.StyleSheet);
            Assert.AreSame(result.ContentType, "text/css");

            result = new MergerResult("", "", "", WebAssetType.Script);
            Assert.AreSame(result.ContentType, "text/javascript");
        }

        [Test]
        public void Should_Get_Hash_Code()
        {
            var result = new MergerResult("test", "1.1", "", WebAssetType.None);

            Assert.AreEqual(449337885, result.GetHashCode());
        }
    }
}
