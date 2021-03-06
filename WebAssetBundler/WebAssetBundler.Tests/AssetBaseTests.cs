﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
        public void Should_Get_Content()
        {
            var webAsset = new AssetBaseImpl("test");
            var stream = webAsset.OpenStream();

            Assert.IsInstanceOf<MemoryStream>(stream);
            Assert.AreEqual(0, stream.Position);
            Assert.AreEqual("test", stream.ReadToEnd());
        }

        [Test]
        public void Should_Modify_Asset()
        {
            var asset = new AssetBaseImpl("test");
            asset.Modify(new ModTest());

            Assert.AreEqual("test1", asset.OpenStream().ReadToEnd());
        }

        [Test]
        public void Should_Return_Open_Stream_Everytime()
        {
            var asset = new AssetBaseImpl("test");

            asset.Modify(new ModTest());
            var stream = asset.OpenStream();

            asset.Modify(new ModTest());
            var streamTwo = asset.OpenStream();

            Assert.AreEqual("test1", stream.ReadToEnd());
            Assert.AreEqual("test11", streamTwo.ReadToEnd());
        }

        [Test]
        public void Should_Close_Internal_Stream()
        {
            string root = TestHelper.RootPath;
            var file = new FileSystemFile(root + "/Files/AssetFileTest.css");
            var asset = new FileSystemAsset(file);

            asset.OpenStream();
            Assert.DoesNotThrow(() => asset.OpenStream());
        }

        public class ModTest : IAssetModifier
        {
            public Stream Modify(Stream openStream)
            {
                var content = openStream.ReadToEnd();
                content = content + 1;
                return content.ToStream();
            }
        }
    }
}
