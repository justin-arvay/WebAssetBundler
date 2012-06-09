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

        [Test]
        public void Should_Resole_Virtual_Path_To_Url()
        {
            var urlResolver = new Mock<IUrlResolver>();
            var writer = new Mock<TextWriter>();
            var tagWriter = new ScriptTagWriter(urlResolver.Object);
            var path = "~/Files/test/css";

            var results = new List<ResolverResult>();
            results.Add(new ResolverResult(path, false, null));
            results.Add(new ResolverResult(path, false, null));

            tagWriter.Write(writer.Object, results);

            urlResolver.Verify(m => m.Resolve(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Write_To_Writer()
        {
            var urlResolver = new Mock<IUrlResolver>();
            var writer = new Mock<TextWriter>();
            var tagWriter = new ScriptTagWriter(urlResolver.Object);            

            var results = new List<ResolverResult>();
            results.Add(new ResolverResult("", false, null));
            results.Add(new ResolverResult("", false, null));

            tagWriter.Write(writer.Object, results);

            writer.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Should_Format_Tag_Correctly()
        {
            var urlResolver = new Mock<IUrlResolver>();
            var writer = new Mock<TextWriter>();
            var tagWriter = new ScriptTagWriter(urlResolver.Object);

            var results = new List<ResolverResult>();
            results.Add(new ResolverResult("", false, null));
            results.Add(new ResolverResult("", false, null));

            tagWriter.Write(writer.Object, results);

            var tag = "<script type=\"text/javascript\" src=\"\"></script>";
            writer.Verify(m => m.WriteLine(It.Is<string>(s => s.Equals(tag))), Times.Exactly(2));
        }
    }
}
