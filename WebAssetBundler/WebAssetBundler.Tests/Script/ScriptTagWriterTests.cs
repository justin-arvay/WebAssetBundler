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
        private Mock<IUrlGenerator<ScriptBundle>> urlGenerator;
        private Mock<TextWriter> textWriter;
        private ScriptTagWriter tagWriter;
        private BundleContext context;
        private ScriptBundle bundle;

        [SetUp]
        public void SetUp()
        {
            urlGenerator = new Mock<IUrlGenerator<ScriptBundle>>();
            textWriter = new Mock<TextWriter>();
            tagWriter = new ScriptTagWriter(urlGenerator.Object);
            context = new BundleContext();
            bundle = new ScriptBundle();
        }

        [Test]
        public void Should_Generate_Url()
        {
            bundle.Host = "http://www.test.com";

            tagWriter.Write(textWriter.Object, bundle, context);

            urlGenerator.Verify(m => m.Generate("asdf", bundle.Hash.ToHexString(), "http://www.test.com", context), Times.Exactly(1));
        }

        [Test]
        public void Should_Write_To_Writer()
        {
            bundle.Host = "http://dev.test.com";

            urlGenerator.Setup(u => u.Generate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<BundleContext>()))
                .Returns("http://dev.test.com/");

            tagWriter.Write(textWriter.Object, bundle, context);

            textWriter.Verify(m => m.WriteLine("<script type=\"text/javascript\" src=\"http://dev.test.com/\"></script>"), Times.Exactly(2));   
        }
    }
}
