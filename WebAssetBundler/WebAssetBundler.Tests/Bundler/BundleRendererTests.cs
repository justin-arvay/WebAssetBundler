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
    public class BundleRendererTests
    {
        private BundleRenderer<BundleImpl> renderer;
        private Mock<ITagWriter<BundleImpl>> tagWriter;

        [SetUp]
        public void Setup()
        {
            tagWriter = new Mock<ITagWriter<BundleImpl>>();
            renderer = new BundleRenderer<BundleImpl>(tagWriter.Object);
        }

        [Test]
        public void Should_Render_Bundle()
        {
            var bundle = new BundleImpl();
            bundle.Name = "test";
            var state = new BundlerState();

            renderer.Render(bundle, state);

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), bundle));
            Assert.True(state.IsRendered(bundle));
        }

        [Test]
        public void Should_Render_All_Bundles()
        {
            var bundle = new BundleImpl();
            bundle.Name = "test";

            var bundleTwo = new BundleImpl();
            bundleTwo.Name = "testTwo";

            var state = new BundlerState();
            var bundles = new List<BundleImpl>()
            {
                bundle,
                bundleTwo
            };

            renderer.RenderAll(bundles, state);

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), bundle));
            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), bundleTwo));
            Assert.True(state.IsRendered(bundle));
            Assert.True(state.IsRendered(bundleTwo));
        }        
    }
}
