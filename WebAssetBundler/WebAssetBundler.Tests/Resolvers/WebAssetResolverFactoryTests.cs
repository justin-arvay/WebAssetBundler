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
    using WebAssetBundler.Web.Mvc;
    using Moq;

    [TestFixture]
    public class WebAssetResolverFactoryTests
    {
        private IWebAssetResolverFactory factory;
        private WebAssetGroup group;

        [SetUp]
        public void Setup()
        {
            group = new WebAssetGroup("", false);
            factory = new WebAssetResolverFactory();            
        }

        [Test]
        public void Should_Return_Group_Resolver()
        {
            Assert.IsInstanceOf<WebAssetGroupResolver>(factory.Create(new WebAssetGroup("", false) { Combine = false }));
        }

        [Test]
        public void Should_Return_Combined_Group_Resolver()
        {
            var group = new WebAssetGroup("", false) { Combine = true };

            Assert.IsInstanceOf<CombinedWebAssetGroupResolver>(factory.Create(group));
        }

        [Test]
        public void Should_Return_Versioned_Group_Resolver()
        {
            Assert.IsInstanceOf<VersionedWebAssetGroupResolver>(factory.Create(new WebAssetGroup("", false) { Combine = false, Version = "1.1" }));
        }

        [Test]
        public void Should_Return_Do_Nothing_Resolver()
        {
            var group = new WebAssetGroup("", false) { Enabled = false };
            Assert.IsInstanceOf<DoNothingWebAssetResolver>(factory.Create(group));
        }       
    }
}
