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

    [TestFixture]
    public class ImageHelperTests
    {
        [Test]
        public void Should_Get_Content_Types()
        {
            Assert.AreEqual("image/png", ImageHelper.GetContentType("/test.png"));
            Assert.AreEqual("image/gif", ImageHelper.GetContentType("/test.gif"));
            Assert.AreEqual("image/jpeg", ImageHelper.GetContentType("/test.jpeg"));
            Assert.AreEqual("image/jpeg", ImageHelper.GetContentType("/test.jpg"));
            Assert.AreEqual("image/jpeg", ImageHelper.GetContentType("/test.jpe"));
            Assert.AreEqual("image/tiff", ImageHelper.GetContentType("/test.tiff"));
            Assert.AreEqual("image/tiff", ImageHelper.GetContentType("/test.tif"));
            Assert.AreEqual("image/bmp", ImageHelper.GetContentType("/test.bmp"));
        }

        [Test]
        public void Should_Create_Name()
        {
            var name = ImageHelper.CreateBundleName("~/test/image.png");

            Assert.AreEqual("0cda79a4083842efcc13b2042d35eadb-image-png", name);
        }
    }
}
