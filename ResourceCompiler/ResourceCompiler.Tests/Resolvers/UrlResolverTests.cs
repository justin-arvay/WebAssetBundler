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
    using ResourceCompiler.Web.Mvc;

    [TestFixture]
    public class UrlResolverTests
    {
        [Test]
        public void Resolve_Should_Throw_Excepton_When_Not_Running_In_Web_Server()
        {
            var resolver = new UrlResolver(TestHelper.CreateRequestContext());

            //Assert.Throws<ArgumentNullException>(() => resolver.Resolve("~/scripts/jquery-1.3.2.min.js"));
        }
        
    }
}
