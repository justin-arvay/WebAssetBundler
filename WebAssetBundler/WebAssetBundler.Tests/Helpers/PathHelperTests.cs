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
    using System;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PathHelperTests
    {
        [Test]
        public void Should_Ignore_Single_Dot()
        {
            Assert.AreEqual("test/file.css", PathHelper.NormalizePath("test/./file.css"));
        }

        [Test]
        public void Should_Handle_Double_Dot()
        {
            Assert.AreEqual("file.css", PathHelper.NormalizePath("test/../file.css"));
        }

        [Test]
        public void Should_Convert_Back_Slashes_To_Forward_Slash()
        {
            Assert.AreEqual("test/file.css", PathHelper.NormalizePath("test\\file.css"));
        }

        [Test]
        public void Should_Keep_Forward_Slash_At_Start()
        {
            Assert.AreEqual("/test/file.css", PathHelper.NormalizePath("/test/file.css"));
        }
    }
}
