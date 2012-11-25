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
    public class FromDirectoryAssetLocatorTests
    {
        private FromDirectoryAssetLocator locator;
        private FromDirectoryComponent component;
        private Mock<HttpServerUtilityBase> server;

        [SetUp]
        public void Setup()
        {
            server = new Mock<HttpServerUtilityBase>();
            locator = new FromDirectoryAssetLocator(server.Object, "");
            component = new FromDirectoryComponent("Files/Configuration", "css");

            server.Setup(m => m.MapPath(It.IsAny<string>()))
               .Returns((string mappedPath) => mappedPath);
        }

        [Test]
        public void Should_Get_All_Files_With_Correct_Extension()
        {
            var assets = locator.Locate(component);
            Assert.AreEqual(3, assets.Count);

        }

        [Test]
        public void Should_Get_Files_That_Start_With()
        {
            component.StartsWithCollection.Add("First");
            component.StartsWithCollection.Add("Second");

            var assets = locator.Locate(component);

            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Get_Files_That_End_With()
        {
            component.EndsWithCollection.Add("File");

            var assets = locator.Locate(component);

            Assert.AreEqual(2, assets.Count);
        }

        [Test]
        public void Should_Get_Files_That_Contain()
        {
            component.ContainsCollection.Add("File.min");

            var assets = locator.Locate(component);

            Assert.AreEqual(1, assets.Count);
        }
    }
}
