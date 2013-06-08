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
    using System.Web;
    using TinyIoC;
    using System.Collections;
    using System.Collections.Generic;

    [TestFixture]
    public class BundlerFactoryTests
    {
        private Mock<HttpContextBase> context;
        private BundlerFactory factory;
        private TinyIoCContainer container;
        private IDictionary items;

        [SetUp]
        public void Setup()
        {
            container = new TinyIoCContainer();
            container.Register<IBundleProvider<BundleImpl>>((new Mock<IBundleProvider<BundleImpl>>()).Object);
            container.Register<IBundleRenderer<BundleImpl>>((new Mock<IBundleRenderer<BundleImpl>>()).Object);
            container.Register<IBundleDependencyResolver<BundleImpl>>((new Mock<IBundleDependencyResolver<BundleImpl>>()).Object);

            items = new Dictionary<object, object>();

            context = new Mock<HttpContextBase>();
            context.Setup(c => c.Items)
                .Returns(items);

            factory = new BundlerFactory(container);
        }

        [Test]
        public void Should_Create_Bundler()
        {
            var bundler = factory.Create<BundlerBaseImpl, BundleImpl>(context.Object);

            Assert.IsInstanceOf<BundlerBaseImpl>(bundler);
            Assert.IsInstanceOf<BundlerState>(bundler.State);
            Assert.IsInstanceOf<BundlerState>(items[bundler.GetType().Name]);
        }

        [Test]
        public void Should_Override_State_Each_Request()
        {
            // create first request bunder and state
            var bundler = factory.Create<BundlerBaseImpl, BundleImpl>(context.Object);

            // change context and items
            var newItems = new Dictionary<object, object>();

            // simulates new request
            context = new Mock<HttpContextBase>();
            context.Setup(c => c.Items)
                .Returns(newItems);

            // create second request bunder and state
            var bundlerTwo = factory.Create<BundlerBaseImpl, BundleImpl>(context.Object);

            Assert.AreNotSame(bundler.State, bundlerTwo.State);
        }

    }
}
