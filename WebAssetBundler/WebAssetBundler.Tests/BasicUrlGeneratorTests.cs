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
    public class BasicUrlGeneratorTests
    {
        private BasicUrlGenerator<BundleImpl> generator;
        private BundleImpl bundle;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            bundle = new BundleImpl();
            bundle.Extension = "css";
            bundle.Hash = new byte[1];
            generator = new BasicUrlGenerator<BundleImpl>(settings);
        }

        [Test]
        public void Should_Generate_Url_Without_Host()
        {
            bundle.Name = "test";
            bundle.Assets.Add(new AssetBaseImpl("1"));

            var url = generator.Generate(bundle);

            Assert.AreEqual("/wab.axd/css/00/test", url);
        }

        [Test]
        public void Should_Generate_Url_With_Host()
        {
            bundle.Name = "test";
            bundle.Host = "http://www.google.ca";
            bundle.Assets.Add(new AssetBaseImpl("1"));

            var url = generator.Generate(bundle);

            Assert.AreEqual("http://www.google.ca/wab.axd/css/00/test", url);
        }

        [Test]
        public void Should_Generate_Cache_Breaker_Url()
        {
            bundle.Name = "test";
            bundle.Assets.Add(new AssetBaseImpl("1"));

            settings.DebugMode = true;

            var url = generator.Generate(bundle);

            Assert.AreEqual("/wab.axd/css/00" + DateTime.Now.ToString("MMddyyHmmss") + "/test", url);
        }
    }
}
