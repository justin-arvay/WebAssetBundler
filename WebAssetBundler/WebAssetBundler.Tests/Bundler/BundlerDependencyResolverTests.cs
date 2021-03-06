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
        public void Should_Get_Required_Bundles_Recursively_And_Order_Correctly()
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

            Assert.AreEqual("BundleSeven", bundles[0].Name);
            Assert.AreEqual("BundleFive", bundles[1].Name);
            Assert.AreEqual("BundleThree", bundles[2].Name);
            Assert.AreEqual("BundleSix", bundles[3].Name);
            Assert.AreEqual("BundleFour", bundles[4].Name);
            Assert.AreEqual("BundleTwo", bundles[5].Name);                                                        
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
        public void Should_Resolve_Referenced()
        {
            /** Referenced Bundles **/

            var bundleOne = new BundleImpl();
            bundleOne.Name = "BundleOne";
            bundleOne.Required.Add("BundleThree");

            var bundleTwo = new BundleImpl();
            bundleTwo.Name = "BundleTwo";
            bundleTwo.Required.Add("BundleFour");
            bundleTwo.Required.Add("BundleSix");

            /** Required Bundles **/
            var bundleThree = new BundleImpl();
            bundleThree.Name = "BundleThree";
            bundleThree.Required.Add("BundleFive");
            bundleThree.Required.Add("BundleSeven");

            var bundleFour = new BundleImpl();
            bundleFour.Name = "BundleFour";
            bundleFour.Required.Add("BundleFive");


            var bundleFive = new BundleImpl();
            bundleFive.Name = "BundleFive";

            var bundleSix = new BundleImpl();
            bundleSix.Name = "BundleSix";

            var bundleSeven = new BundleImpl();
            bundleSeven.Name = "BundleSeven";

            //referenced bundle list
            var bundles = new List<BundleImpl>()
            {
                bundleOne,
                bundleTwo
            };

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

            var results = (IList<BundleImpl>)resolver.ResolveReferenced(bundles);

            //should reverse ordering when resolving
            //should remove bundle 5, keeping the last bundle resolved, or first after reversing
            //should recursively get required bundles
            Assert.AreSame(bundleSix, results[0]);
            Assert.AreSame(bundleFive, results[1]);
            Assert.AreSame(bundleFour, results[2]);
            Assert.AreSame(bundleTwo, results[3]);
            Assert.AreSame(bundleSeven, results[4]);
            Assert.AreSame(bundleThree, results[5]);
            Assert.AreSame(bundleOne, results[6]);

        }
    }
}
