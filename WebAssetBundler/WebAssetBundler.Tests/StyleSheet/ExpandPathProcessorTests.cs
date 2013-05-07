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
    using System.Web;
    using System.IO;

    [TestFixture]
    public class ExpandPathProcessorTests
    {
        private ExpandPathProcessor processor;
        private StyleSheetBundle bundle;
        private SettingsContext settings;
        private AssetBaseImpl asset;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext();

            asset = new AssetBaseImpl();
            bundle = new StyleSheetBundle();
            bundle.Assets.Add(asset);
            processor = new ExpandPathProcessor(settings);
        }        

        //---------------------------------------------------------------------------------------------

        [Test]
        public void Should_Filter_Relative_Path()
        {
            var stream = "url(../img/test.jpg)".ToStream();
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);
            var returnStream = processor.Modify(stream);

            Assert.AreEqual("url(../../../../img/test.jpg", returnStream.ReadToEnd());
        }

        [Test]
        public void Should_Not_Filter_Absolute_Path()
        {
            asset.StreamContent = "url(/img/test.jpg)";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);
            var stream = processor.Modify(asset.Content);

            Assert.AreEqual(null, stream.ReadToEnd());
        }

        [Test]
        public void Should_Not_Filter_When_Http_Domain()
        {
            asset.StreamContent = "url(http://www.google.com/img/test.jpg)";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);
            var stream = processor.Modify(asset.Content);

            Assert.AreEqual(null, stream.ReadToEnd());
        }

        [Test]
        public void Should_Not_Filter_When_Https_Domain()
        {
            asset.StreamContent = "url(https://www.google.com/img/test.jpg)";
            bundle.Url = "/a/a/a/a";

            processor.Process(bundle);
            var stream = processor.Modify(asset.Content);

            Assert.AreEqual(null, stream.ReadToEnd());
        }
    }
}
