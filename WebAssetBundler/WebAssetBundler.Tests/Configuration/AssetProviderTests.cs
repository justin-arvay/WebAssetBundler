﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

    [TestFixture]
    public class AssetProviderTests
    {
        private AssetProvider provider;
        private FromDirectoryComponent component;
        private Mock<HttpServerUtilityBase> server;
        private BundleContext context;

        [SetUp]
        public void Setup()
        {
            context = new BundleContext();
            server = new Mock<HttpServerUtilityBase>();
            provider = new AssetProvider(server.Object, "D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\", context);
            component = new FromDirectoryComponent("Files/Configuration", "css");
        }

        [Test]
        public void Should_Get_All_Files_With_Correct_Extension_As_Virtual_Paths()
        {            

            server.Setup(m => m.MapPath("Files/Configuration"))
                .Returns("D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\Files\\Configuration\\");

            server.Setup(m => m.MapPath("~/Files/Configuration/FirstFile.css"))
                .Returns((string path) => path);

            server.Setup(m => m.MapPath("~/Files/Configuration/SecondFile.css"))
                .Returns((string path) => path);

            server.Setup(m => m.MapPath("~/Files/Configuration/ThirdFile.min.css"))
                .Returns((string path) => path);

            var assets = (IList<AssetBase>)provider.GetAssets(component);

            Assert.AreEqual("~/Files/Configuration/FirstFile.css", assets[0].Source, "0 index");
            Assert.AreEqual("~/Files/Configuration/SecondFile.css", assets[1].Source, "0 index");
            Assert.AreEqual("~/Files/Configuration/ThirdFile.min.css", assets[2].Source, "0 index");
            Assert.AreEqual(3, assets.Count);

        }

        [Test]
        public void Should_Get_Files_That_Start_With()
        {
            server.Setup(m => m.MapPath("Files/Configuration"))
                .Returns("D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\Files\\Configuration\\");

            server.Setup(m => m.MapPath("~/Files/Configuration/FirstFile.css"))
                .Returns((string path) => path);

            server.Setup(m => m.MapPath("~/Files/Configuration/SecondFile.css"))
                .Returns((string path) => path);

            component.StartsWithCollection.Add("First");
            component.StartsWithCollection.Add("Second");

            var assets = (IList<AssetBase>)provider.GetAssets(component);

            Assert.AreEqual(2, assets.Count);
            Assert.AreEqual("~/Files/Configuration/FirstFile.css", assets[0].Source, "0 index");
            Assert.AreEqual("~/Files/Configuration/SecondFile.css", assets[1].Source, "0 index");
        }

        [Test]
        public void Should_Get_Files_That_End_With()
        {
            server.Setup(m => m.MapPath("Files/Configuration"))
                .Returns("D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\Files\\Configuration\\");

            server.Setup(m => m.MapPath("~/Files/Configuration/FirstFile.css"))
                .Returns((string path) => path);

            server.Setup(m => m.MapPath("~/Files/Configuration/SecondFile.css"))
                .Returns((string path) => path);

            component.EndsWithCollection.Add("File");

            var assets = (IList<AssetBase>)provider.GetAssets(component);

            Assert.AreEqual(2, assets.Count);
            Assert.AreEqual("~/Files/Configuration/FirstFile.css", assets[0].Source, "0 index");
            Assert.AreEqual("~/Files/Configuration/SecondFile.css", assets[1].Source, "0 index");
        }

        [Test]
        public void Should_Get_Files_That_Contain()
        {
            server.Setup(m => m.MapPath("Files/Configuration"))
                .Returns("D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\Files\\Configuration\\");

            server.Setup(m => m.MapPath("~/Files/Configuration/ThirdFile.min.css"))
                .Returns((string path) => path);

            component.ContainsCollection.Add("File.min");

            var assets = (IList<AssetBase>)provider.GetAssets(component);

            Assert.AreEqual(1, assets.Count);
            Assert.AreEqual("~/Files/Configuration/ThirdFile.min.css", assets[0].Source);
        }

        [Test]
        public void Should_Get_Asset()
        {
            server.Setup(m => m.MapPath("~/File.css"))
                .Returns((string path) => path);

            var asset = provider.GetAsset("~/File.css");

            Assert.AreEqual("~/File.css", asset.Source);
        }

        [Test]
        public void Should_Get_Asset_That_Combines_Path_With_Default()
        {
            server.Setup(m => m.MapPath("~/Content/File.css"))
                .Returns((string path) => path);

            context.DefaultPath = "~/Content/";
            var asset = provider.GetAsset("File.css");

            Assert.AreEqual("~/Content/File.css", asset.Source);
        }
    }
}
