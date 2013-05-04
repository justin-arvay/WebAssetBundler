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
    public class UrlAssignmentProcessorTests
    {
        private UrlAssignmentProcessor<BundleImpl> processor;
        private Mock<IUrlGenerator<BundleImpl>> urlGenerator;

        [SetUp]
        public void Setup()
        {
            urlGenerator = new Mock<IUrlGenerator<BundleImpl>>();
            processor = new UrlAssignmentProcessor<BundleImpl>(urlGenerator.Object);
        }

        [Test]
        public void Should_Generate_Url()
        {
            var bundle = new BundleImpl();
            var url = "/wab.axd/css/asd/file-css";

            urlGenerator.Setup(g => g.Generate(bundle)).Returns(url);

            processor.Process(bundle);

            Assert.AreEqual(url, bundle.Url);
            urlGenerator.Verify(g => g.Generate(bundle));
        }
    }
}
