﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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
    public class AssetFileTests
    {
        private AssetFile file;
        private Mock<HttpServerUtilityBase> server;

        [SetUp]
        public void Setup()
        {
            server = new Mock<HttpServerUtilityBase>();
            file = new AssetFile("~/Files/AssetFileTest.css", server.Object);

            server.Setup(s => s.MapPath("~/Files/AssetFileTest.css"))
                .Returns("D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\Files\\AssetFileTest.css");
        }

        [Test]
        public void Should_Get_Full_Path()
        {

            Assert.AreEqual(
                "D:\\ASP.NET Projects\\WebAssetBundler\\WebAssetBundler\\WebAssetBundler.Tests\\Files\\AssetFileTest.css", 
                file.FullPath);
        }

        [Test]
        public void Should_Open_Stream()
        {

        }

        [Test]
        public void Should_Exist()
        {
            Assert.True(file.Exists);
        }
    }
}