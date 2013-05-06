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
    using System.Collections.Generic;

    [TestFixture]
    public class BackgroundImageReaderTests
    {
        private BackgroundImageReader reader;

        [SetUp]
        public void Setup()
        {
            reader = new BackgroundImageReader();
        }

        [Test]
        public void Should_Match_Url_With_Double_Quotes()
        {
            var stream = "url(\"/img/test.jpg\");".ToStream();
            var paths = new List<string>(reader.ReadAll(stream));

            Assert.AreEqual("/img/test.jpg", paths[0]);
        }

        [Test]
        public void Should_Match_Url_With_Single_Quotes()
        {
            var stream = "url('/img/test.jpg');".ToStream();
            var paths = new List<string>(reader.ReadAll(stream));

            Assert.AreEqual("/img/test.jpg", paths[0]);
        }

        [Test]
        public void Should_Match_Url_With_No_Quotes()
        {
            var stream = "url(/img/test.jpg);".ToStream();
            var paths = new List<string>(reader.ReadAll(stream));

            Assert.AreEqual("/img/test.jpg", paths[0]);
        }

        [Test]
        public void Should_Match_Url_With_Spacing()
        {
            var stream = "url ( /img/test.jpg );".ToStream();
            var paths = new List<string>(reader.ReadAll(stream));

            Assert.AreEqual("/img/test.jpg", paths[0]);
        }
    }
}
