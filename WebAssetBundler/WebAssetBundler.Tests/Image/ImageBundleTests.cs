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

    [TestFixture]
    public class ImageBundleTests
    {
        private ImageBundle bundle;

        [SetUp]
        public void Setup()
        {
            bundle = new ImageBundle("image/png", "~/test/image.png"); 
        }

        [Test]
        public void Should_Set_Values()
        {
            Assert.AreEqual("image/png", bundle.ContentType);
            Assert.AreEqual("~/test/image.png", bundle.Url);
            Assert.AreEqual("0cda79a4083842efcc13b2042d35eadb-image-png", bundle.Name);
        }

        [Test]
        public void Should_Return_Asset_Separator()
        {
            Assert.AreEqual("", bundle.AssetSeparator);
        }
    }
}
