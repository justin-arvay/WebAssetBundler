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
    using WebAssetBundler.Web.Mvc;
    using Moq;

    [TestFixture]
    public class WebAssetResolverFactoryTests
    {
        private IWebAssetResolverFactory factory;
        private BundleImpl bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new BundleImpl();
            factory = new WebAssetResolverFactory();            
        }

        [Test]
        public void Should_Return_Bundle_Resolver()
        {
            bundle.Combine = false;
            Assert.IsInstanceOf<WebAssetBundleResolver>(factory.Create(bundle));
        }

        [Test]
        public void Should_Return_Combined_Bundle_Resolver()
        {
            bundle.Combine = true;
            Assert.IsInstanceOf<CombinedWebAssetBundleResolver>(factory.Create(bundle));
        }

        [Test]
        public void Should_Return_Do_Nothing_Resolver()
        {
            bundle.Enabled = false;
            Assert.IsInstanceOf<DoNothingWebAssetResolver>(factory.Create(bundle));
        }       
    }
}
