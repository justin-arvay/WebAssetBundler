// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void Should_Be_Equal_Using_Case_Insensitive_Check()
        {
            string name = "John";

            Assert.True(name.IsCaseInsensitiveEqual("jOHN"));
        }

        [Test]
        public void Should_Be_Equal_Using_Case_Sensitive_Check()
        {
            string name = "John";

            Assert.True(name.IsCaseSensitiveEqual("John"));
        }

        [Test]
        public void Should_Be_Able_To_Format_String()
        {
            string format = "{0} - {1}";
            Assert.AreEqual("t - s", format.FormatWith("t", "s"));
        }

        [Test]
        public void Should_Find_Correct_Number_Of_Occurances()
        {
            string value = "This is the best string.";

            Assert.AreEqual(2, value.CountOccurance("is"));
        }
    }
}
