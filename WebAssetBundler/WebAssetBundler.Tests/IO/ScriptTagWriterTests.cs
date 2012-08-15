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

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.IO;

    public class ScriptTagWriterTests
    {
        private Mock<IUrlResolver> resolver;
        private Mock<TextWriter> textWriter;
        private ScriptTagWriter tagWriter;

        [SetUp]
        public void SetUp()
        {
            resolver = new Mock<IUrlResolver>();
            textWriter = new Mock<TextWriter>();
            tagWriter = new ScriptTagWriter(resolver.Object);
        }

        [Test]
        public void Should_Resole_Virtual_Path_To_Url()
        {
            var results = new List<WebAssetMergerResult>();
            results.Add(new WebAssetMergerResult("", ""));
            results.Add(new WebAssetMergerResult("", ""));

            tagWriter.Write(textWriter.Object, results);

            resolver.Verify(m => m.Resolve(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Write_To_Writer()
        {
            var results = new List<WebAssetMergerResult>();
            results.Add(new WebAssetMergerResult("", ""));
            results.Add(new WebAssetMergerResult("", ""));

            tagWriter.Write(textWriter.Object, results);

            textWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Format_Tag_Correctly()
        {
            var results = new List<WebAssetMergerResult>();
            results.Add(new WebAssetMergerResult("", ""));
            results.Add(new WebAssetMergerResult("", ""));

            tagWriter.Write(textWriter.Object, results);

            var tag = "<script type=\"text/javascript\" src=\"\"></script>";
            textWriter.Verify(m => m.WriteLine(It.Is<string>(s => s.Equals(tag))), Times.Exactly(2));
        }
    }
}
