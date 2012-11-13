﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System;
    using WebAssetBundler.Web.Mvc;
    using System.Collections.Generic;

    [TestFixture]
    public class WebAssetBundleResolverTests
    {
        private WebAssetBundleResolver resolver;
        private Bundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
            resolver = new WebAssetBundleResolver(bundle);
        }

        [Test]
        public void Should_Resolve_A_List_Of_Sources_For_Each_Item_In_Group()
        {
            bundle.Assets.Add(new WebAsset("~/Files/test.css"));
            bundle.Assets.Add(new WebAsset("~/Files/test2.css"));

            Assert.AreEqual(2, resolver.Resolve().Count);
        }
        

        [Test]
        public void Should_Resolve_Compress_For_Result()
        {
            bundle.Compress = true;
            bundle.Assets.Add(new WebAsset(""));

            var results = resolver.Resolve();

            Assert.IsTrue(results[0].Compress);
        }


        [Test]
        public void Should_Resolve_Name_For_Result()
        {
            bundle.Assets.Add(new WebAsset("~/Files/test-file.css"));
       
            var results = resolver.Resolve();

            Assert.AreEqual("test-file", results[0].Name);
        }

        [Test]
        public void Should_Resolve_Host()
        {
            bundle.Host = "1.1.1.1";

            bundle.Assets.Add(new WebAsset("~/Files/test-file.css"));

            var results = resolver.Resolve();

            Assert.AreEqual("1.1.1.1", results[0].Host);
        }
    }
}