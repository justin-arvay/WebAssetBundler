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
    using System;
    
    [TestFixture]
    public class ImageUrlGeneratorTests
    {
        private ImageUrlGenerator generator;
        private SettingsContext settings;
        private ImageBundle bundle;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext();
            generator = new ImageUrlGenerator(settings);
            bundle = new ImageBundle("img/png");
            bundle.Hash = new byte[1];
        }

        [Test]
        public void Should_Generate_Url()
        {
            bundle.Name = "asdf-test-png";
            bundle.Assets.Add(new AssetBaseImpl());
            bundle.Url = "~/Image/img.png";

            var url = generator.Generate(bundle);

            Assert.AreEqual("/wab.axd/image/00/asdf-test-png", url);
        }

        [Test]
        public void Should_Generate_Cache_Breaker_Url()
        {
            bundle.Name = "asdf-test-png";
            bundle.Assets.Add(new AssetBaseImpl());
            bundle.Url = "~/Image/img.png";

            settings.DebugMode = true;

            var url = generator.Generate(bundle);

            Assert.AreEqual("/wab.axd/image/00" + DateTime.Now.ToString("MMddyyHmmss") + "/asdf-test-png", url);
        }
    }
}

