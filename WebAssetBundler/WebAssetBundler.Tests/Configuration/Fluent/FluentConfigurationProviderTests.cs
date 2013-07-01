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
    using System.Collections.Generic;

    [TestFixture]
    public class FluentConfigurationProviderTests
    {
        private FluentConfigurationProvider provider;
        private Mock<IFluentConfigurationFactory> factory;
        private Mock<ITypeProvider> typeProvider;
        private Mock<IDirectorySearchFactory> searchFactory;
        private Mock<IAssetProvider> assetProvider;

        [SetUp]
        public void Setup()
        {
            typeProvider = new Mock<ITypeProvider>();
            factory = new Mock<IFluentConfigurationFactory>();
            searchFactory = new Mock<IDirectorySearchFactory>();
            assetProvider = new Mock<IAssetProvider>();
            provider = new FluentConfigurationProvider(typeProvider.Object, assetProvider.Object, searchFactory.Object, factory.Object);
        }

        [Test]
        public void Should_Get_Configuration()
        {
            var config = new Mock<IFluentConfiguration<BundleImpl>>();

            var types = new List<Type>();
            types.Add(typeof(IFluentConfiguration<BundleImpl>));
            types.Add(typeof(IFluentConfiguration<BundleImpl>));

            factory.Setup(f => f.Create<BundleImpl>(It.IsAny<Type>()))
                .Returns(config.Object);

            typeProvider.Setup(t => t.GetImplementationTypes(typeof(IFluentConfiguration<BundleImpl>)))
                .Returns(types);

            var configs = provider.GetConfigurations<BundleImpl>();

            Assert.AreEqual(2, configs.Count);
            factory.Verify(f => f.Create<BundleImpl>(It.IsAny<Type>()));
            config.VerifySet(c =>c.AssetProvider = It.IsAny<IAssetProvider>());
            config.VerifySet(v => v.DirectorySearchFactory = It.IsAny<IDirectorySearchFactory>());
            config.VerifySet(v => v.Bundle = It.IsAny<BundleImpl>());
            config.Verify(c => c.Configure());
        }
    }
}
