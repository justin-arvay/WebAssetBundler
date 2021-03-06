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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;
    using Moq;


#if MVC2 || MVC3
    class ValueProvider : IValueProvider
    {
        private readonly IDictionary<string, ValueProviderResult> data;

        public ValueProvider(IDictionary<string, ValueProviderResult> data)
        {
            this.data = data;
        }

        public bool ContainsPrefix(string prefix)
        {
            throw new System.NotImplementedException();
        }

        public ValueProviderResult GetValue(string key)
        {
            ValueProviderResult result;
            bool found = data.TryGetValue(key, out result);
            if (found)
            {
                return result;
            }
            
            return null;
        }

    }
#endif

    public class ControllerTestDouble : Controller
    {
        public ControllerTestDouble(IDictionary<string, ValueProviderResult> valueProviderData, ViewDataDictionary viewData)
        {
#if MVC1
            ValueProvider = valueProviderData;
#endif
#if MVC2 || MVC3
            ValueProvider = new ValueProvider(valueProviderData);
#endif
            ViewData = viewData;
            ControllerContext = new ControllerContext(TestHelper.CreateRequestContext(), this);
        }
    }


    public static class TestHelper
    {
        public const string AppPathModifier = "/$(SESSION)";
        public const string ApplicationPath = "/app/";

        public static HtmlHelper CreateHtmlHelper()
        {
            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();

            viewDataContainer.SetupGet(container => container.ViewData).Returns(new ViewDataDictionary());

            ViewContext viewContext = TestHelper.CreateViewContext();

            HtmlHelper helper = new HtmlHelper(viewContext, viewDataContainer.Object);

            return helper;
        }

        public static HtmlHelper<TModel> CreateHtmlHelper<TModel>() where TModel : class, new()
        {
            return CreateHtmlHelper(new TModel());
        }

        public static HtmlHelper<TModel> CreateHtmlHelper<TModel>(TModel value)
        {
            var viewDataContainer = new Mock<IViewDataContainer>();

            viewDataContainer.SetupGet(container => container.ViewData).Returns(new ViewDataDictionary { Model = value });

            var viewContext = CreateViewContext();

            var helper = new HtmlHelper<TModel>(viewContext, viewDataContainer.Object);

            return helper;
        }

        private static Mock<HttpContextBase> httpContext;
        private static object sync = new object();

        public static Mock<HttpContextBase> CreateMockedHttpContext(bool createNew)
        {
            if (createNew)
            {
                return MockedHttpContext();
            }

            if (httpContext == null)
            {
                lock (sync)
                {
                    if (httpContext == null)
                    {
                        httpContext = MockedHttpContext();
                    }
                }
            }

            httpContext.Setup(context => context.Items).Returns(new Hashtable());

            return httpContext;
        }

        public static Mock<HttpContextBase> CreateMockedHttpContext()
        {
            return CreateMockedHttpContext(false);
        }

        private static Mock<HttpContextBase> MockedHttpContext()
        {
            Mock<HttpContextBase> result = new Mock<HttpContextBase>();
            result.Setup(context => context.Server).Returns(new Mock<HttpServerUtilityBase>().Object);
            result.Setup(context => context.Request.AppRelativeCurrentExecutionFilePath).Returns("~/");
            result.Setup(context => context.Request.ApplicationPath).Returns(ApplicationPath);
            result.Setup(context => context.Request.Url).Returns(new Uri("http://localhost"));
            result.Setup(context => context.Request.PathInfo).Returns(string.Empty);
            result.Setup(context => context.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns((TextWriter tw) => new HtmlTextWriter(tw));
            result.Setup(context => context.Request.Browser.EcmaScriptVersion).Returns(new Version("5.0"));
            result.Setup(context => context.Request.Browser.SupportsCss).Returns(true);
            result.Setup(context => context.Request.Browser.MajorVersion).Returns(7);
            result.Setup(context => context.Request.Browser.IsBrowser("IE")).Returns(false);
            result.Setup(context => context.Request.QueryString).Returns(new NameValueCollection());
            result.Setup(context => context.Request.Headers).Returns(new NameValueCollection { { "Accept-Encoding", "gzip" } });
            result.Setup(context => context.Items).Returns(new Hashtable());
            result.Setup(context => context.Response.Output).Returns(new Mock<TextWriter>().Object);

            result.Setup(context => context.Response.Filter).Returns(new Mock<Stream>().Object);
            // ReSharper disable AccessToStaticMemberViaDerivedType
            result.Setup(context => context.Response.ContentEncoding).Returns(UTF8Encoding.Default);
            // ReSharper restore AccessToStaticMemberViaDerivedType
            result.Setup(context => context.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => AppPathModifier + r);
            return result;
        }
        public static RequestContext CreateRequestContext()
        {
            return new RequestContext(CreateMockedHttpContext().Object, new RouteData
            {
                Values =
                    {
                        {"controller", "home"},
                        {"action", "index"}
                    }
            });
        }

        public static void RegisterDummyRoutes(RouteCollection routes)
        {
            routes.Clear();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("ProductList", "Products", new { controller = "Product", action = "List" });
            routes.MapRoute("ProductDetail", "Products/Detail/{id}", new { controller = "Product", action = "Detail", id = string.Empty });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = string.Empty });
        }

        public static ViewContext CreateViewContext()
        {
            return new ViewContext(
                CreateControllerContext(), 
                new Mock<IView>().Object, 
                new ViewDataDictionary(), 
                new TempDataDictionary(), 
                new StringWriter());
        }

        public static ControllerContext CreateControllerContext()
        {
            return new ControllerContext(CreateRequestContext(), new ControllerTestDouble(new Dictionary<string,
                ValueProviderResult>(), new Mock<IViewDataContainer>().Object.ViewData));
        }
        public static void RegisterDummyRoutes()
        {
            RegisterDummyRoutes(RouteTable.Routes);
        }

        public static string RootPath
        {
            get
            {
                return PathHelper.NormalizePath(AppDomain.CurrentDomain.BaseDirectory + "/../../");
            }
        }
    }
}
