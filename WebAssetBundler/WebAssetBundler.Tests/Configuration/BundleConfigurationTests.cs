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

            bundleConfig.AssetProvider = assetProvider.Object;
        }

        [Test]
        public void Should_Add_Asset()
        {
            bundleConfig.Add("");

            Assert.AreEqual(1, bundleConfig.GetBundle().Assets.Count);
        }

        [Test]
        public void Should_Set_Combine()
        {
            bundleConfig.Combine(true);

            Assert.IsTrue(bundleConfig.GetBundle().Combine);
        }

        [Test]
        public void Should_Set_Compress()
        {
            bundleConfig.Compress(true);

            Assert.IsTrue(bundleConfig.GetBundle().Compress);
        }

        [Test]
        public void Should_Set_Name()
        {
            bundleConfig.Name("name");

            Assert.AreEqual("name", bundleConfig.GetBundle().Name);
        }

        [Test]
        public void Should_Set_Host()
        {
            bundleConfig.Host("http://www.test.com");

            Assert.AreEqual("http://www.test.com", bundleConfig.GetBundle().Host);
        }

        [Test]
        public void Should_Set_Name_Of_Class()
        {
            Assert.AreEqual("BundleConfigurationImpl", bundleConfig.GetBundle().Name);
        }

        [Test]
        public void Should_Locate_Filtered_Assets_In_Directory()
        {
            var assets = new List<AssetBase>();
            assets.Add(new AssetBaseImpl());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<FromDirectoryComponent>()))
                .Returns(assets);


            bundleConfig.AddFromDirectory("~/Files/Configration", b => b.ToString());

            Assert.AreEqual(1, bundleConfig.GetBundle().Assets.Count);
        }

        [Test]
        public void Should_Add_All_Assets_In_Directory()
        {
            var assets = new List<AssetBase>();
            assets.Add(new AssetBaseImpl());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<FromDirectoryComponent>()))
                .Returns(assets);

            bundleConfig.AddFromDirectory("~/Files/Configration");

            Assert.AreEqual(1, bundleConfig.GetBundle().Assets.Count);
        }
    }
}
