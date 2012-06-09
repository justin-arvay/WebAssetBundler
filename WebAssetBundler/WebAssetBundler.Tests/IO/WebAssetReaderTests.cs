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
    using System;
    using System.Web.Hosting;
    using System.Web;
    using Moq;

    [TestFixture]
    public class WebAssetReaderTests
    {
        [Test]
        public void Should_Read_Content_From_Web_Asset()
        {
            var server = new Mock<HttpServerUtilityBase>();   
            var reader = new WebAssetReader(server.Object);
            var webAsset = new WebAsset("Files/read.txt");

            server.Setup(m => m.MapPath(It.IsAny<string>()))
                .Returns((string mappedPath) => mappedPath);

            Assert.AreEqual("Line 1", reader.Read(webAsset));
        }
    }
}
