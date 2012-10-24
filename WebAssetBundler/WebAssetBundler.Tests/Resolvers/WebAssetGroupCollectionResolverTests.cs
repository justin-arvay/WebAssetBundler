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
    public class WebAssetGroupCollectionResolverTests
    {
        private Mock<IWebAssetResolverFactory> factory;
        private WebAssetGroupCollectionResolver resolver;
        private WebAssetGroupCollection collection;
        private BuilderContext context;
        private Mock<IWebAssetResolver> internalResolver;

        [SetUp]
        public void Setup()
        {
            factory = new Mock<IWebAssetResolverFactory>();
            resolver = new WebAssetGroupCollectionResolver(factory.Object);
            collection = new WebAssetGroupCollection();
            context = new BuilderContext();

            internalResolver = new Mock<IWebAssetResolver>();
            internalResolver.Setup(f => f.Resolve()).Returns(new List<ResolverResult>());
            factory.Setup(f => f.Create(It.IsAny<WebAssetGroup>())).Returns(internalResolver.Object);
        }

        [Test]
        public void Should_Resolve_Collection_And_Return_Results()
        {                     
            var group = new WebAssetGroup("test", false);

            group.Assets.Add(new WebAsset("path/test.css"));
            collection.Add(group);

            var results = resolver.Resolve(collection, context);

            internalResolver.Verify(i => i.Resolve(), Times.Once());
            factory.Verify(f => f.Create(It.IsAny<WebAssetGroup>()), Times.Once());
        }

        [Test]
        public void Should_Override_Group_Combined()
        {
            var group = new WebAssetGroup("test", false)
            {
                Combine = true
            };

            context.DebugMode = true;
            context.EnableCombining = false;
            collection.Add(group);
            
            resolver.Resolve(collection, context);

            Assert.AreEqual(false, group.Combine);

        }

        [Test]
        public void Should_Override_Group_Compress()
        {
            var group = new WebAssetGroup("test", false)
            {
                Compress = true
            };

            context.DebugMode = true;
            context.EnableCompressing = false;
            collection.Add(group);

            resolver.Resolve(collection, context);

            Assert.AreEqual(false, group.Combine);
        }
    }
}