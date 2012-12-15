// WebAssetBundler - Bundles web assets so you dont have to.
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

    [TestFixture]
    public class WebAssetBundleCollectionResolverTests
    {
        private Mock<IWebAssetResolverFactory> factory;
        private WebAssetBundleCollectionResolver resolver;
        private BundleCollection<Bundle> collection;
        private BundleContext context;
        private Mock<IWebAssetResolver> internalResolver;
        private Bundle bundle;

        [SetUp]
        public void Setup()
        {
            factory = new Mock<IWebAssetResolverFactory>();
            resolver = new WebAssetBundleCollectionResolver(factory.Object);
            collection = new BundleCollection<Bundle>();
            context = new BundleContext();

            internalResolver = new Mock<IWebAssetResolver>();
            internalResolver.Setup(f => f.Resolve()).Returns(new List<ResolvedBundle>());
            factory.Setup(f => f.Create(It.IsAny<Bundle>())).Returns(internalResolver.Object);
            bundle = new BundleImpl();
        }

        [Test]
        public void Should_Resolve_Collection_And_Return_Results()
        {

            bundle.Assets.Add(new AssetBase("path/test.css"));
            collection.Add(bundle);

            var results = resolver.Resolve(collection, context);

            internalResolver.Verify(i => i.Resolve(), Times.Once());
            factory.Verify(f => f.Create(It.IsAny<Bundle>()), Times.Once());
        }
    }
}