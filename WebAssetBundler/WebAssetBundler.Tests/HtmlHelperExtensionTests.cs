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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Moq;
    using WebAssetBundler;
    using System.Web;
    using System.IO;

    [TestFixture]
    public class HtmlHelperExtensionTests
    {
        [Test]
        public void Should_Return_Instance()
        {
            DefaultSettings.ConfigurationFactory = new DoNothingConfigurationFactory();
            HttpRequest httpRequest = new HttpRequest("", "http://mySomething/", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponce = new HttpResponse(stringWriter);            

            HttpContext.Current = new HttpContext(httpRequest, httpResponce);
            var helper = TestHelper.CreateHtmlHelper();
            
            Assert.IsInstanceOf<ComponentBuilder>(helper.Bundler());            
        }
    }
}
