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
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class StreamExtensionsTests
    {
        [Test]
        public void Should_Read_To_End()
        {
            var stream = "test".ToStream();
            Func<Stream> func = () =>
            {
                return stream;
            };

            func = Run(func);
            func = Run(func);
            func = Run(func);

            var end = func();


            Assert.AreEqual("test", "test".ToStream().ReadToEnd());
        }

        public Func<Stream> Run(Func<Stream> streamOpen)
        {
            return delegate
            {
                var stream = streamOpen();
                var byteArray = new byte[stream.Length];
                stream.Read(byteArray, 1, 1);

                return stream;
            };
        }
    }
}
