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
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class MinifyProcessorTests
    {        
        private MinifyProcessor<BundleImpl> processor;
        private Mock<IMinifier> minifier;
        private BundleImpl bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
            minifier = new Mock<IMinifier>();
            processor = new MinifyProcessor<BundleImpl>(minifier.Object, ".min");
        }

        [Test]
        public void Should_Add_Minify_Modifier()
        {
            var asset = new AssetBaseImpl("var value = 1;");
            asset.Source = "~/file.js";

            bundle.Assets.Add(asset);
            bundle.Minify = true;

            processor.Process(bundle);

            Assert.AreEqual(1, bundle.Assets[0].Modifiers.Count);
        }

        [Test]
        public void Should_Not_Add_Minify_Modifier()
        {
            var asset = new AssetBaseImpl("var value = 1;");

            bundle.Assets.Add(asset);
            bundle.Minify = false;

            processor.Process(bundle);

            Assert.AreEqual(0, bundle.Assets[0].Modifiers.Count);
        }

        [Test]
        public void Should_Not_Add_Minify_Modifier_When_Already_Minified()
        {
            var asset = new AssetBaseImpl("var value = 1;");
            asset.Source = "~/file.min.js";

            bundle.Assets.Add(asset);
            bundle.Minify = true;

            processor.Process(bundle);

            Assert.AreEqual(0, bundle.Assets[0].Modifiers.Count);
        }
    }    
}
