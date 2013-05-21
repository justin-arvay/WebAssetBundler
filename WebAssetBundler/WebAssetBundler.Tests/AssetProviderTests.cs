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
    using TinyIoC;

    [TestFixture]
    public class AssetProviderTests
    {
        private AssetProvider provider;
        private DirectorySearch context;
        private IDirectory directory;
        private SettingsContext settings;
        private string root;
        private IDirectory rootDirectory;

        [SetUp]
        public void Setup()
        {
            root = PathHelper.NormalizePath(AppDomain.CurrentDomain.BaseDirectory + "/../../");
            rootDirectory = new FileSystemDirectory(root);
            var container = new TinyIoCContainer();

            settings = new SettingsContext(false, ".min");
            settings.AppRootDirectory = rootDirectory;

            container.Register(settings);

            provider = new AssetProvider(settings);

            context = new DirectorySearch();
        }

        [Test]
        public void Should_Get_All_Files_With_Correct_Extension()
        {
            context.Patterns = new List<string>() { "*.css" };

            var assets = (IList<AssetBase>)provider.GetAssets("~/Files/AssetProvider/Mixed", context);

            Assert.AreEqual(root + "/Files/AssetProvider/Mixed\\FirstFile.min.css", assets[0].Source);
            Assert.AreEqual(root + "/Files/AssetProvider/Mixed\\SecondFile.min.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Get_Raw_Assets()
        {
            context.Patterns = new List<string>() { "*.css" };

            var assets = (IList<AssetBase>)provider.GetAssets("~/Files/AssetProvider/Raw", context);

            Assert.AreEqual(root + "/Files/AssetProvider/Raw\\FirstFile.css", assets[0].Source);
            Assert.AreEqual(root + "/Files/AssetProvider/Raw\\SecondFile.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Always_Get_Raw_Assets_In_Debug_Mode()
        {
            settings.DebugMode = true;
            context.Patterns = new List<string>() { "*.css" };

            var assets = (IList<AssetBase>)provider.GetAssets("~/Files/AssetProvider/Mixed", context);

            Assert.AreEqual(root + "/Files/AssetProvider/Mixed\\FirstFile.css", assets[0].Source);
            Assert.AreEqual(root + "/Files/AssetProvider/Mixed\\SecondFile.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Get_Minified_Assets()
        {
            context.Patterns = new List<string>() { "*.css" };

            var assets = (IList<AssetBase>)provider.GetAssets("~/Files/AssetProvider/Mixed", context);

            Assert.AreEqual(root + "/Files/AssetProvider/Mixed\\FirstFile.min.css", assets[0].Source);
            Assert.AreEqual(root + "/Files/AssetProvider/Mixed\\SecondFile.min.css", assets[1].Source);
            Assert.AreEqual(2, assets.Count);
        }


        [Test]
        public void Should_Get_Asset()
        {
            var file = new FileSystemFile(root + "/Files/AssetProvider/Mixed/FirstFile.css");
            var directory = new Mock<IDirectory>();

            directory.Setup(d => d.GetFile("~/File.css"))
                .Returns(file);

            settings.AppRootDirectory = directory.Object;

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual(root + "/Files/AssetProvider/Mixed/FirstFile.min.css", asset.Source);
        }

        [Test]
        public void Should_Get_Raw_Asset()
        {
            var file = new FileSystemFile(root + "/Files/AssetProvider/Raw/FirstFile.css");
            var directory = new Mock<IDirectory>();

            directory.Setup(d => d.GetFile("~/File.css"))
                .Returns(file);

            settings.AppRootDirectory = directory.Object;

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual(root + "/Files/AssetProvider/Raw/FirstFile.css", asset.Source);
        }

        [Test]
        public void Should_Always_Get_Raw_Asset_In_Debug_Mode()
        {
            var file = new FileSystemFile(root + "/Files/AssetProvider/Mixed/FirstFile.min.css"); 
            var directory = new Mock<IDirectory>();

            directory.Setup(d => d.GetFile("~/File.min.css"))
                .Returns(file);

            settings.DebugMode = true;
            settings.AppRootDirectory = directory.Object;

            var asset = provider.GetAsset("~/File.min.css");

            Assert.AreEqual(root + "/Files/AssetProvider/Mixed/FirstFile.css", asset.Source);
        }

        [Test]
        public void Should_Get_MinifiedAsset()
        {
            var file = new FileSystemFile(root + "/Files/AssetProvider/Mixed/FirstFile.css");
            var directory = new Mock<IDirectory>();

            directory.Setup(d => d.GetFile("~/File.css"))
                .Returns(file);

            settings.AppRootDirectory = directory.Object;

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual(root + "/Files/AssetProvider/Mixed/FirstFile.min.css", asset.Source);
        }

        [Test]
        public void Should_Throw_Exception_When_Getting_Asset_That_Does_Not_Exist()
        {
            var file = new FileSystemFile(root + "/test.css");
            var directory = new Mock<IDirectory>();

            directory.Setup(d => d.GetFile("~/test.css"))
                .Returns(file);

            settings.AppRootDirectory = directory.Object;

            Assert.Throws<FileNotFoundException>(() => provider.GetAsset("~/test.css"));
        }

        [Test]
        public void Should_Throw_Exception_When_Getting_Assets_From_A_Directory_That_Does_Not_Exist()
        {
            context = new DirectorySearch();

            Assert.Throws<DirectoryNotFoundException>(() => provider.GetAssets("~/FakeDir", context));
        }
    }
}
