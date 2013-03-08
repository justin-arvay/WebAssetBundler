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
        private BundleImpl bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
            processor = new UrlAssignmentProcessor<BundleImpl>(() => false);
        }

        [Test]
        public void Should_Generate_Url_Without_Host()
        {
            bundle.Name = "test";
            bundle.Content = "1";

            processor.Process(bundle);

            Assert.AreEqual("/wab.axd/css/c4ca4238a0b923820dcc509a6f75849b/test", bundle.Url);
        }

        [Test]
        public void Should_Generate_Url_With_Host()
        {
            bundle.Name = "test";
            bundle.Host = "http://www.google.ca";
            bundle.Content = "1";

            processor.Process(bundle);

            Assert.AreEqual("http://www.google.ca/wab.axd/css/c4ca4238a0b923820dcc509a6f75849b/test", bundle.Url);
        }

        [Test]
        public void Should_Generate_Cache_Breaker_Url()
        {
            bundle.Name = "test";
            bundle.Content = "1";

            processor = new UrlAssignmentProcessor<BundleImpl>(() => true);
            processor.Process(bundle);

            Assert.AreEqual("/wab.axd/css/c4ca4238a0b923820dcc509a6f75849b" + DateTime.Now.ToString("MMddyyHmmss") + "/test", bundle.Url);
        }
    }
}
