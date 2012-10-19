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

    [TestFixture]
    public class HttpHandlerFactoryTests
    {
        private HttpHandlerFactory factory;
        private Mock<HttpContextBase> httpContext;

        [SetUp]
        public void Setup()
        {
            httpContext = new Mock<HttpContextBase>();
            factory = new HttpHandlerFactory();
        }

        [Test]
        public void Should_Throw_Exception()
        {
            httpContext.Setup(c => c.Request.PathInfo).Returns("");
            Assert.Throws<HttpException>(() => factory.Create(httpContext.Object));
        }

        [Test]
        public void Should_Return_Script_Handler()
        {
            httpContext.Setup(c => c.Request.PathInfo).Returns("/script");
            Assert.IsInstanceOf<AssetHttpHandler>(factory.Create(httpContext.Object));
        }

        [Test]
        public void Should_Return_Style_Sheet_Handler()
        {
            httpContext.Setup(c => c.Request.PathInfo).Returns("/style-sheet");
            Assert.IsInstanceOf<AssetHttpHandler>(factory.Create(httpContext.Object));
        }
    }
}
