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
    using System.Web;
    using System.Collections.Generic;
    using System;

    [TestFixture]
    public class AssetProviderTests
    {
        private AssetProvider provider;
        private DirectorySearchContext context;
        private Mock<HttpServerUtilityBase> server;
        private Mock<IDirectoryFactory> directoryFactory;
        private IDirectory directory;

        [SetUp]
        public void Setup()
        {            
            server = new Mock<HttpServerUtilityBase>();
            directoryFactory = new Mock<IDirectoryFactory>();
            provider = new AssetProvider(directoryFactory.Object, server.Object, () => ".min", () => false);
            context = new DirectorySearchContext("~/Files/AssetProvider/Mixed", "css");

            directory = new FileSystemDirectory("../../Files/AssetProvider/Mixed");

            directoryFactory.Setup(d => d.Create("~/Files/AssetProvider/Mixed"))
                .Returns(directory);
        }

        [Test]
        public void Should_Get_All_Files_With_Correct_Extension()
        {
            var assets = (IList<AssetBase>)provider.GetAssets(context);

            Assert.AreEqual("../../Files/AssetProvider/Mixed\\FirstFile.css", assets[0].Source);
            Assert.AreEqual("../../Files/AssetProvider/Mixed\\SecondFile.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Get_Raw_Assets()
        {
            context = new DirectorySearchContext("~/Files/AssetProvider/Raw", "css");

            directory = new FileSystemDirectory("../../Files/AssetProvider/Raw");

            directoryFactory.Setup(d => d.Create("~/Files/AssetProvider/Raw"))
                .Returns(directory);

            var assets = (IList<AssetBase>)provider.GetAssets(context);

            Assert.AreEqual("../../Files/AssetProvider/Raw\\FirstFile.css", assets[0].Source);
            Assert.AreEqual("../../Files/AssetProvider/Raw\\SecondFile.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Always_Get_Raw_Assets_In_Debug_Mode()
        {
            provider = new AssetProvider(directoryFactory.Object, server.Object, () => ".min", () => true);

            var assets = (IList<AssetBase>)provider.GetAssets(context);

            Assert.AreEqual("../../Files/AssetProvider/Mixed\\FirstFile.css", assets[0].Source);
            Assert.AreEqual("../../Files/AssetProvider/Mixed\\SecondFile.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Get_MinifiedAssets()
        {
            var assets = (IList<AssetBase>)provider.GetAssets(context);

            Assert.AreEqual("../../Files/AssetProvider/Mixed\\FirstFile.min.css", assets[0].Source);
            Assert.AreEqual("../../Files/AssetProvider/Mixed\\SecondFile.min.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }


        [Test]
        public void Should_Get_Asset()
        {
            server.Setup(m => m.MapPath("~/File.css"))
                .Returns((string path) => path);

            var asset = provider.GetAsset("~/File.min.css");

            Assert.AreEqual("~/File.min.css", asset.Source);
        }

        [Test]
        public void Should_Throw_Exception_If_Not_Virtual_Source()
        {
            server.Setup(m => m.MapPath("~/Content/File.css"))
                .Returns((string path) => path);

            Assert.Throws<ArgumentException>(() => provider.GetAsset("File.css"));

            server.Setup(m => m.MapPath("/Content/File.css"))
                .Returns((string path) => path);

            Assert.Throws<ArgumentException>(() => provider.GetAsset("File.css"));

        }

        [Test]
        public void Should_Get_Raw_Asset()
        {
        }

        [Test]
        public void Should_Always_Get_Raw_Asset_In_Debug_Mode()
        {
        }

        [Test]
        public void Should_Get_MinifiedAsset()
        {
        }
    }
}
