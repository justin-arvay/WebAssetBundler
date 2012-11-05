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

    [TestFixture]
    public class BundleConfigurationTests
    {
        private BundleConfiguration<BundleConfigurationImpl, BundleImpl> bundleConfig;

        [SetUp]
        public void Setup()
        {
            bundleConfig = new BundleConfigurationImpl();
        }

        [Test]
        public void Should_Add_Asset()
        {
            bundleConfig.Add("");

            Assert.AreEqual(1, bundleConfig.GetBundle().Assets.Count());
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
    }
}
