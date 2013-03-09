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
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;

    [TestFixture]    
    public class BundleConfigurationTests
    {
        private BundleConfigurationImpl bundleConfig;
        private Mock<IAssetProvider> assetProvider;

        [SetUp]
        public void Setup()
        {
            assetProvider = new Mock<IAssetProvider>();
            bundleConfig = new BundleConfigurationImpl();
            bundleConfig.Bundle = new BundleImpl();

            bundleConfig.AssetProvider = assetProvider.Object;
        }

        [Test]
        public void Should_Add_Asset()
        {
            bundleConfig.Add("");

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Throw_Exception_If_Asset_Already_Exists()
        {
        }

        [Test]
        public void Should_Set_Compress()
        {
            bundleConfig.Compress(true);

            Assert.IsTrue(bundleConfig.Bundle.Minify);
        }

        [Test]
        public void Should_Set_Name()
        {
            bundleConfig.Name("name");

            Assert.AreEqual("name", bundleConfig.Bundle.Name);
        }

        [Test]
        public void Should_Set_Host()
        {
            bundleConfig.Host("http://www.test.com");

            Assert.AreEqual("http://www.test.com", bundleConfig.Bundle.Host);
        }

        [Test]
        public void Should_Locate_Filtered_Assets_In_Directory()
        {
            var assets = new List<AssetBase>();
            assets.Add(new AssetBaseImpl());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<DirectorySearchContext>()))
                .Returns(assets);


            bundleConfig.AddFromDirectory("~/Files/Configration", b => b.ToString());

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Add_All_Assets_In_Directory()
        {
            var assets = new List<AssetBase>();
            assets.Add(new AssetBaseImpl());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<DirectorySearchContext>()))
                .Returns(assets);

            bundleConfig.AddFromDirectory("~/Files/Configration");

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Not_Add_Duplicate_Assets_From_Directory()
        {
        }

        [Test]
        public void Should_Not_Add_Duplicate_Filtered_Assets_From_Directory()
        { 
        }


    }
}
