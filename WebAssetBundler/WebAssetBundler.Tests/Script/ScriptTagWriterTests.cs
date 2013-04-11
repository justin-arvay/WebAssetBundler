﻿// WebAssetBundler - Bundles web assets so you dont have to.
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
        private Mock<TextWriter> textWriter;
        private ScriptTagWriter tagWriter;
        private ScriptBundle bundle;

        [SetUp]
        public void SetUp()
        {
            textWriter = new Mock<TextWriter>();
            tagWriter = new ScriptTagWriter();
            bundle = new ScriptBundle();
        }

        [Test]
        public void Should_Write_To_Writer()
        {
            bundle.Url = "/test";

            tagWriter.Write(textWriter.Object, bundle);

            textWriter.Verify(m => m.WriteLine("<script type=\"text/javascript\" src=\"/test\"></script>"), Times.Exactly(1));   
        }

        [Test]
        public void Should_Write_External_Tag()
        {
            bundle.Url = "/test";
            bundle.Assets.Add(new ExternalAsset()
            {
                Source = "http://www.google.com/file.js"
            });

            tagWriter.Write(textWriter.Object, bundle);

            textWriter.Verify(m => m.WriteLine("<script type=\"text/javascript\" src=\"http://www.google.com/file.js\"></script>"), Times.Exactly(1));   
        }
    }
}
