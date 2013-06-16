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
            provider = new FluentConfigurationProvider(typeProvider.Object, assetProvider.Object, searchFactory.Object, factory.Object);
        }

        [Test]
        public void Should_Get_Configuration()
        {
            var types = new List<Type>();
            types.Add(typeof(FluentConfigurationImpl));
            types.Add(typeof(FluentConfigurationImpl));

            factory.Setup(f => f.Create<BundleImpl>(It.IsAny<Type>()))
                .Returns(new FluentConfigurationImpl());

            typeProvider.Setup(t => t.GetImplementationTypes(typeof(IFluentConfiguration<BundleImpl>)))
                .Returns(types);

            var configs = provider.GetConfigurations<BundleImpl>();

            Assert.AreEqual(2, configs.Count);
            factory.Verify(f => f.Create<BundleImpl>(It.IsAny<Type>()));
            Assert.AreSame(assetProvider.Object, configs[0].AssetProvider);
            Assert.AreSame(searchFactory.Object, configs[0].DirectorySearchFactory);
            Assert.IsInstanceOf<BundleMetadata>(configs[0].Metadata);
        }
    }
}
