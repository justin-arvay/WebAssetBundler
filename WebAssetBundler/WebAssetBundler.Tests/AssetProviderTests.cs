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
    using System.IO;

    [TestFixture]
    public class AssetProviderTests
    {
        private AssetProvider provider;
        private DirectorySearchContext context;
        private Mock<HttpServerUtilityBase> server;
        private Mock<IDirectoryFactory> directoryFactory;
        private IDirectory directory;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            var container = new TinyIoCContainer();
            Bundle test = new BundleImpl();

            settings = new SettingsContext(false, ".min");
            container.Register(settings);
            
            server = new Mock<HttpServerUtilityBase>();
            directoryFactory = new Mock<IDirectoryFactory>();
//            provider = new AssetProvider(directoryFactory.Object, server.Object, (t) =>
//            {
////                return settings as SettingsContext<Bundle>;
//            });

            context = new DirectorySearchContext("~/Files/AssetProvider/Mixed", "css");

            directory = new FileSystemDirectory("../../Files/AssetProvider/Mixed");

            directoryFactory.Setup(d => d.Create("~/Files/AssetProvider/Mixed"))
                .Returns(directory);
        }

        [Test]
        public void Should_Get_All_Files_With_Correct_Extension()
        {
            var assets = (IList<AssetBase>)provider.GetAssets(context);

            Assert.AreEqual("../../Files/AssetProvider/Mixed\\FirstFile.min.css", assets[0].Source);
            Assert.AreEqual("../../Files/AssetProvider/Mixed\\SecondFile.min.css", assets[1].Source);
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
            settings = new SettingsContext(true, ".min");

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
                .Returns("../../Files/AssetProvider/Mixed/FirstFile.css");

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual("../../Files/AssetProvider/Mixed/FirstFile.min.css", asset.Source);
        }

        [Test]
        public void Should_Throw_Exception_If_Not_Virtual_Source()
        {

            Assert.Throws<ArgumentException>(() => provider.GetAsset("File.css"));

            Assert.Throws<ArgumentException>(() => provider.GetAsset("File.css"));

        }

        [Test]
        public void Should_Get_Raw_Asset()
        {
            server.Setup(m => m.MapPath("~/File.css"))
                .Returns("../../Files/AssetProvider/Raw/FirstFile.css");

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual("../../Files/AssetProvider/Raw/FirstFile.css", asset.Source);
        }

        [Test]
        public void Should_Always_Get_Raw_Asset_In_Debug_Mode()
        {
            settings = new SettingsContext(true, ".min");

            server.Setup(m => m.MapPath("~/File.min.css"))
                .Returns("../../Files/AssetProvider/Mixed/FirstFile.min.css");

            var asset = provider.GetAsset("~/File.min.css");

            Assert.AreEqual("../../Files/AssetProvider/Mixed/FirstFile.css", asset.Source);
        }

        [Test]
        public void Should_Get_MinifiedAsset()
        {
            server.Setup(m => m.MapPath("~/File.css"))
                .Returns("../../Files/AssetProvider/Mixed/FirstFile.css");

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual("../../Files/AssetProvider/Mixed/FirstFile.min.css", asset.Source);
        }

        [Test]
        public void Should_Throw_Exception_When_Getting_Asset_That_Does_Not_Exist()
        {
            Assert.Throws<FileNotFoundException>(() => provider.GetAsset("~/test.css"));
        }

        [Test]
        public void Should_Throw_Exception_When_Getting_Assets_From_A_Directory_That_Does_Not_Exist()
        {
            context = new DirectorySearchContext("~/FakeDir", "css");
            directoryFactory.Setup(d => d.Create("~/FakeDir"))
               .Returns(new FileSystemDirectory("../../FakeDir"));

            Assert.Throws<DirectoryNotFoundException>(() => provider.GetAssets(context));
        }
    }
}
