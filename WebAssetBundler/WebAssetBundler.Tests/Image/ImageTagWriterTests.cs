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
    using System;
    using NUnit.Framework;
    using System.IO;
    using System.Web.UI;
    using Moq;

    [TestFixture]
    public class ImageTagWriterTests
    {
        private ImageTagWriter writer;
        private Mock<TextWriter> textWriter;

        [SetUp]
        public void Setup()
        {
            writer = new ImageTagWriter();
            textWriter = new Mock<TextWriter>();
        }

        [Test]
        public void Should_Write_Tag()
        {
            var bundle = new ImageBundle("");
            bundle.Height = 99;
            bundle.Width = 98;
            bundle.Alt = "this is alt";
            bundle.Url = "/wab.axd/image/asdasd/image-png";

            writer.Write(textWriter.Object, bundle);

            textWriter.Verify(w => w.Write("<img src=\"/wab.axd/image/asdasd/image-png\" alt=\"this is alt\" height=\"99\" width=\"98\" />"));
        }
    }
}
