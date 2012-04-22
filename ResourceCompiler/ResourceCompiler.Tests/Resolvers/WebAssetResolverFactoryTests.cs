// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;
    using ResourceCompiler.Web.Mvc;
    using Moq;

    [TestFixture]
    public class WebAssetResolverFactoryTests
    {
        private Mock<IPathResolver> pathResolver;

        public WebAssetResolverFactoryTests()
        {
            pathResolver = new Mock<IPathResolver>();
        }

        [Test]
        public void Should_Return_Group_Resolver()
        {
            var factory = new WebAssetResolverFactory(pathResolver.Object);

            Assert.IsInstanceOf<WebAssetGroupResolver>(factory.Create(new WebAssetGroup("", false, "")));
        }

        [Test]
        public void Should_Return_Combined_Group_Resolver()
        {
            var factory = new WebAssetResolverFactory(pathResolver.Object);
            var group = new WebAssetGroup("", false, "") { Combine = true };

            Assert.IsInstanceOf<CombinedWebAssetGroupResolver>(factory.Create(group));
        }
    }
}
