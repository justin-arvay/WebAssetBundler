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
    using System.IO;
    using System.Collections.Generic;

    [TestFixture]
    public class BundlerBaseTests
    {
        private BundlerBaseImpl bundler;
        private Mock<IBundleProvider<BundleImpl>> provider;
        private Mock<IBundleRenderer<BundleImpl>> renderer;
        private Mock<IBundleDependencyResolver<BundleImpl>> resolver;

        [SetUp]
        public void Setup()
        {
            renderer = new Mock<IBundleRenderer<BundleImpl>>();
            provider = new Mock<IBundleProvider<BundleImpl>>();
            resolver = new Mock<IBundleDependencyResolver<BundleImpl>>();
            bundler = new BundlerBaseImpl(provider.Object, renderer.Object, resolver.Object);
            bundler.State = new BundlerState();
        }

        [Test]
        public void Should_Reference_Bundle()
        {
            bundler.Reference("TestBundle");

            Assert.AreEqual("TestBundle", ((List<string>)bundler.State.ReferencedBundleNames)[0]);
        }               

        [Test]
        public void Should_Render_Referenced()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_Throw_Exception_If_Rendered_Referenced_Already()
        {
            Assert.Fail();
        }
    }
}
