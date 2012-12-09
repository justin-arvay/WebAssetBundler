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
    public class StyleSheetMergeProcessorTests
    {
        private StyleSheetMergeProcessor processor;
        private StyleSheetBundle bundle;

        [SetUp]
        public void Setup()
        {
            processor = new StyleSheetMergeProcessor();
            bundle = new StyleSheetBundle();
        }

        [Test]
        public void Should_Merge()
        {
            bundle.Assets.Add(new AssetBaseImpl());
            bundle.Assets.Add(new AssetBaseImpl());

            processor.Process(bundle);

            Assert.AreEqual(1, bundle.Assets);
            Assert.IsInstanceOf<MergedAsset>(bundle.Assets[0]);
        }
    }
}