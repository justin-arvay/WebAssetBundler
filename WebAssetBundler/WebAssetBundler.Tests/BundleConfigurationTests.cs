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
    using System;

    [TestFixture]    
    public class BundleConfigurationTests
    {
        private BundleConfigurationImpl bundleConfig;
        private Mock<IAssetProvider> assetProvider;
        private Mock<IDirectorySearchFactory> dirSearchFactory;

        [SetUp]
        public void Setup()
        {
            assetProvider = new Mock<IAssetProvider>();
            dirSearchFactory = new Mock<IDirectorySearchFactory>();
            bundleConfig = new BundleConfigurationImpl();
            bundleConfig.Bundle = new BundleImpl();

            bundleConfig.AssetProvider = assetProvider.Object;
            bundleConfig.DirectorySearchFactory = dirSearchFactory.Object;
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
            assetProvider.Setup(p => p.GetAsset("~/Test.css"))
                .Returns(new AssetBaseImpl()
                {
                    Source = "~/Test"
                });

            bundleConfig.Add("~/Test.css");

            Assert.Throws<ArgumentException>(() => bundleConfig.Add("~/Test.css"));
        }

        [Test]
        public void Should_Set_Minify()
        {
            bundleConfig.Minify(true);

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

            dirSearchFactory.Setup(d => d.CreateForType<BundleImpl>("css"))
                .Returns(new DirectorySearch());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<string>(), It.IsAny<DirectorySearch>()))
                .Returns(assets);


            bundleConfig.AddDirectory("~/Files/Configration", b => b.ToString());

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Add_All_Assets_In_Directory()
        {
            var assets = new List<AssetBase>();
            assets.Add(new AssetBaseImpl());

            dirSearchFactory.Setup(d => d.CreateForType<BundleImpl>("css"))
                .Returns(new DirectorySearch());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<string>(), It.IsAny<DirectorySearch>()))
                .Returns(assets);

            bundleConfig.AddDirectory("~/Files/Configration");

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Not_Add_Duplicate_Assets_From_Directory()
        {
            var assets = new List<AssetBase>()
            {
                new AssetBaseImpl()
                {
                    Source = "~/Test"
                },
                new AssetBaseImpl()
                {
                    Source = "~/Test"
                }
            };

            assetProvider.Setup(p => p.GetAssets(It.IsAny<string>(), It.IsAny<DirectorySearch>()))
                .Returns(assets);

            bundleConfig.AddDirectory("~/Test");

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Not_Add_Duplicate_Filtered_Assets_From_Directory()
        {
            var assets = new List<AssetBase>()
            {
                new AssetBaseImpl()
                {
                    Source = "~/Test"
                },
                new AssetBaseImpl()
                {
                    Source = "~/Test"
                }
            };

            assetProvider.Setup(p => p.GetAssets(It.IsAny<string>(), It.IsAny<DirectorySearch>()))
                .Returns(assets);

            bundleConfig.AddDirectory("~/Test", b => b.ToString());

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Locate_Assets_In_Directory_With_User_Created_Directory_Search()
        {
            var assets = new List<AssetBase>();
            assets.Add(new AssetBaseImpl());

            assetProvider.Setup(l => l.GetAssets(It.IsAny<string>(), It.IsAny<DirectorySearch>()))
                .Returns(assets);

            bundleConfig.AddDirectory("~/Files/Configration", new DirectorySearch());

            Assert.AreEqual(1, bundleConfig.Bundle.Assets.Count);
        }

        [Test]
        public void Should_Add_Required_Bundle()
        {
            bundleConfig.Required("TestBundle");

            Assert.AreEqual("TestBundle", ((List<string>)bundleConfig.Bundle.Required)[0]);
        }
    }
}
