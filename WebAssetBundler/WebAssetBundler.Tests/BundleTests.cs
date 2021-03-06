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
    using System;
    using System.IO;

    [TestFixture]
    public class BundleTests
    {
        private Bundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
        }

        [Test]
        public void Should_Be_Empty_By_Default()
        {            
            Assert.AreEqual(0, bundle.Assets.Count);
        }

        [Test]
        public void Should_Have_Minify_On_By_Default()
        {
            Assert.AreEqual(true, bundle.Minify);
        }

        [Test]
        public void Should_Add_Asset()
        {
            bundle.Assets.Add(new AssetBaseImpl());

            Assert.AreEqual(1, bundle.Assets.Count);
        }


        [Test]
        public void Should_Be_Empty_Host()
        {
            Assert.AreEqual(String.Empty, bundle.Host);
        }

        [Test]
        public void Should_Be_External_Bundle()
        {
            var asset = new ExternalAsset();
            bundle.Assets.Add(asset);

            Assert.True(bundle.IsExternal);
        }

        [Test]
        public void Should_Be_1_Year_Browser_Ttl_By_Default()
        {
            Assert.AreEqual(525949, bundle.BrowserTtl);
        }

        [Test]
        public void Should_Return_Content()
        {
            var assetOne = new AssetBaseImpl("test");
            var assetTwo = new AssetBaseImpl("test");

            bundle.Assets.Add(assetOne);
            bundle.Assets.Add(assetTwo);

            //should ignore second asset
            Assert.AreEqual("test", bundle.Content.ReadToEnd());
        }

        [Test]
        public void Should_Have_Zero_Required_Bundles_By_Default()
        {
            Assert.AreEqual(0, bundle.Required.Count);
        }

        [Test]
        public void Should_Modify_Assets()
        {
            var assetOne = new AssetBaseImpl("test");
            var assetTwo = new AssetBaseImpl("test");

            bundle.Assets.Add(assetOne);
            bundle.Assets.Add(assetTwo);

            bundle.Modify(new TestModifier());

            Assert.AreEqual("testtest", assetOne.OpenStream().ReadToEnd());
            Assert.AreEqual("testtest", assetTwo.OpenStream().ReadToEnd());
        }

        private class TestModifier : IAssetModifier
        {
            public Stream Modify(Stream openStream)
            {
                var content = openStream.ReadToEnd();
                content += content;
                return content.ToStream();
            }
        }
    }
}
