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
    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.IO;

    [TestFixture]
    public class BundlerDependencyResolverTests
    {
        private BundleDependencyResolver<BundleImpl> resolver;
        private Mock<IBundleProvider<BundleImpl>> provider;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IBundleProvider<BundleImpl>>();
            resolver = new BundleDependencyResolver<BundleImpl>(provider.Object);
        }

        [Test]
        public void Should_Get_Required_Bundles_Recursively()
        {
            var bundleOne = new BundleImpl();
            bundleOne.Name = "BundleOne";
            bundleOne.Required.Add("BundleTwo");
            bundleOne.Required.Add("BundleThree");

            var bundleTwo = new BundleImpl();
            bundleTwo.Name = "BundleTwo";
            bundleTwo.Required.Add("BundleFour");
            bundleTwo.Required.Add("BundleSix");

            var bundleThree = new BundleImpl();
            bundleThree.Name = "BundleThree";
            bundleThree.Required.Add("BundleFive");
            bundleThree.Required.Add("BundleSeven");

            var bundleFour = new BundleImpl();
            bundleFour.Name = "BundleFour";

            var bundleFive = new BundleImpl();
            bundleFive.Name = "BundleFive";

            var bundleSix = new BundleImpl();
            bundleSix.Name = "BundleSix";

            var bundleSeven = new BundleImpl();
            bundleSeven.Name = "BundleSeven";

            provider.Setup(p => p.GetNamedBundle(bundleOne.Name))
                .Returns(bundleOne);

            provider.Setup(p => p.GetNamedBundle(bundleTwo.Name))
                .Returns(bundleTwo);

            provider.Setup(p => p.GetNamedBundle(bundleThree.Name))
                .Returns(bundleThree);

            provider.Setup(p => p.GetNamedBundle(bundleFour.Name))
                .Returns(bundleFour);

            provider.Setup(p => p.GetNamedBundle(bundleFive.Name))
                .Returns(bundleFive);

            provider.Setup(p => p.GetNamedBundle(bundleSix.Name))
                .Returns(bundleSix);

            provider.Setup(p => p.GetNamedBundle(bundleSeven.Name))
                .Returns(bundleSeven);

            var bundles = (IList<BundleImpl>)resolver.Resolve(bundleOne);

            Assert.AreEqual("BundleTwo", bundles[0].Name);
            Assert.AreEqual("BundleFour", bundles[1].Name);
            Assert.AreEqual("BundleSix", bundles[2].Name);
            Assert.AreEqual("BundleThree", bundles[3].Name);
            Assert.AreEqual("BundleFive", bundles[4].Name);
            Assert.AreEqual("BundleSeven", bundles[5].Name);
        }

        [Test]
        public void Should_Throw_Exception_When_Getting_Required_Bundles()
        {
            var bundleOne = new BundleImpl();
            bundleOne.Name = "BundleOne";
            bundleOne.Required.Add("BundleTwo");

            var bundleTwo = new BundleImpl();
            bundleTwo.Name = "BundleTwo";
            bundleTwo.Required.Add("BundleOne");

            provider.Setup(p => p.GetNamedBundle(bundleOne.Name))
                .Returns(bundleOne);

            provider.Setup(p => p.GetNamedBundle(bundleTwo.Name))
                .Returns(bundleTwo);

            //the above should create a cirular reference through bundle names
            //simulates unintentional behavior when configuring bundles wrong.
            //max of 25 depth, however infinate required bundles are allowed

            Assert.Throws<InvalidDataException>(() => resolver.Resolve(bundleOne));
        }

        [Test]
        public void Should_Correct_Bundles_Order()
        {
            var bundleOne = new BundleImpl();
            bundleOne.Name = "BundleOne";

            var bundleTwo = new BundleImpl();
            bundleTwo.Name = "BundleTwo";

            var bundleThree = new BundleImpl();
            bundleThree.Name = "BundleThree";

            var bundleFour = new BundleImpl();
            bundleFour.Name = "BundleFour";

            var bundleFive = new BundleImpl();
            bundleFive.Name = "BundleFive";

            var bundles = (IList<BundleImpl>)resolver.Resolve(bundleOne);

            Assert.AreEqual("BundleFive", bundles[0].Name);
            Assert.AreEqual("BundleFour", bundles[1].Name);
            Assert.AreEqual("BundleThree", bundles[2].Name);
            Assert.AreEqual("BundleTwo", bundles[3].Name);
            Assert.AreEqual("BundleOne", bundles[4].Name);
        }
    }
}
