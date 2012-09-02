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
        public void Should_Set_Path_With_Constructor()
        {
            var path = "some/path/file.css";
            var result = new WebAssetMergerResult(path, "");

            Assert.AreSame(result.Path, path);
        }

        [Test]
        public void Should_Set_Content_With_Constructor()
        {
            var content = "some content";
            var result = new WebAssetMergerResult("", content);

            Assert.AreSame(result.Content, content);
        }

        [Test]
        public void Should_Create_Url()
        {                    
            Assert.AreEqual("192.168.1.1/test.css", WebAssetMergerResult.CreateUrl("/test.css", "192.168.1.1/"), "Ip");
            Assert.AreEqual("http://www.test.com/test.css", WebAssetMergerResult.CreateUrl("test.css", "http://www.test.com/"), "domain");
            Assert.AreEqual("/test.css", WebAssetMergerResult.CreateUrl("/test.css", null), "null host");
            Assert.AreEqual("/test.css", WebAssetMergerResult.CreateUrl("/test.css", ""), "length 0 host");
            Assert.AreEqual("http://www.test.com/test.css", WebAssetMergerResult.CreateUrl("/test.css", "http://www.test.com"), "with \\ to /");
        }
    }
}
