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
    public class BundleProviderBaseTests
    {
        private BundleProviderBase<BundleImpl> provider;
        private SettingsContext settings;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext(false, ".min");
            provider = new BundleProviderBaseImpl(settings);
        }

        [Test]
        public void Should_Get_External_Bundle()
        {
            string source = "http://www.google.com/file.js";
            var bundle = provider.GetExternalBundle(source);

            Assert.True(bundle.IsExternal);
            Assert.AreEqual(1, bundle.Assets.Count);
            Assert.AreEqual(source.ToHash(), bundle.Name);
            Assert.AreEqual(source, bundle.Assets[0].Source);
        }
    }
}
