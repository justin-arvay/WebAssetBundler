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
    using System.Security.Cryptography;
    using System.IO;
    using System.Text;    

    [TestFixture]
    public class ByteArrayExtensionTests
    {

        [Test]
        public void Should_Convert_To_Lower_Case_Hex_String()
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = new byte[] {123, 46, 200, 3, 178, 206, 73, 228, 165, 65, 6, 141, 73, 90, 181, 112};

            Assert.AreEqual("7b2ec803b2ce49e4a541068d495ab570", bytes.ToHexString());
        }
    }
}
