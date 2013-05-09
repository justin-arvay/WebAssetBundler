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
    using Moq;
    using System.IO;
    using System;

    [TestFixture]
    public class AssetBaseTests
    {

        [Test]
        public void Should_Be_Able_To_Get_Name_Without_Ext()
        {
            var name = "file";
            var webAsset = new AssetBaseImpl();
            webAsset.Source = "path/file.css";

            Assert.AreEqual(name, webAsset.Name);
        }

        [Test]
        public void Should_Be_Able_To_Get_Extention()
        {
            var webAsset = new AssetBaseImpl();
            webAsset.Source = "path/file.css";

            Assert.AreEqual("css", webAsset.Extension);
        }

        [Test]
        public void Should_Get_Content_And_Apply_Modifier()
        {
            var webAsset = new AssetBaseImpl("test");
            var modifier = new Mock<IAssetModifier>();

            //simulate a stream that was read in the modifier
            var returnStream = "return test".ToStream();
            returnStream.Position = 4;

            modifier.Setup(m => m.Modify(It.IsAny<Stream>()))
                .Returns(returnStream);

            webAsset.Modifiers.Add(modifier.Object);
            webAsset.Modifiers.Add(modifier.Object);

            var stream = webAsset.Content;

            modifier.Verify(t => t.Modify(It.IsAny<Stream>()), Times.Exactly(2));
            Assert.IsInstanceOf<MemoryStream>(stream);
            Assert.AreEqual(0, stream.Position);
            Assert.AreEqual("return test", stream.ReadToEnd());
        }

        [Test]
        public void Should_Save_Stream()
        {
            Assert.Fail();
        }
    }
}
