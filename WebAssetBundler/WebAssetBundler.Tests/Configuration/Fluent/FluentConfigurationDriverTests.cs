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
    using System.Collections.Generic;
    using System;

    [TestFixture]
    public class FluentConfigurationDriverTests
    {
        public FluentConfiguartionDriver driver;
        private Mock<IFluentConfigurationProvider> provider;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IFluentConfigurationProvider>();
            driver = new FluentConfiguartionDriver(provider.Object);
        }

        [Test]
        public void Should_Load_Bundle()
        {
            var config = new Mock<IFluentConfiguration<BundleImpl>>();
            var configs = new List<IFluentConfiguration<BundleImpl>>();
            configs.Add(config.Object);

            config.Setup(c => c.Bundle)
                .Returns(new BundleImpl()
                {
                    Name = "Test"
                });

            provider.Setup(p => p.GetConfigurations<BundleImpl>())
                .Returns(configs);

            BundleImpl bundle = driver.LoadBundle<BundleImpl>("Test");

            Assert.IsInstanceOf<BundleImpl>(bundle);
        }

        [Test]
        public void Should_Throw_Exception()
        {
            var configs = new List<IFluentConfiguration<BundleImpl>>();

            provider.Setup(p => p.GetConfigurations<BundleImpl>())
                .Returns(configs);

            Assert.Throws<Exception>(() => driver.LoadBundle<BundleImpl>("Test"));
        }
    }
}
