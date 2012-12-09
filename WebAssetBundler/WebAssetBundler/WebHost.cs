﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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
    using System.Web;

    public class WebHost : IDisposable
    {
        private TinyIoCContainer container;

        public WebHost()
        {
            container = new TinyIoCContainer();
        }

        public TinyIoCContainer Container
        {
            get
            {
                return container;
            }
        }

        public void ConfigureContainer()
        {
            container.Register((c, p) => HttpContext());
            container.Register((c, p) => HttpContext().Request);
            container.Register((c, p) => HttpContext().Response);
            container.Register((c, p) => HttpContext().Server);

            container.Register<IWebAssetReader, WebAssetReader>();            

            container.Register<ICacheProvider, CacheProvider>()
                .AsSingleton();
            container.Register<IBundlesCache<StyleSheetBundle>, BundlesCache<StyleSheetBundle>>()
                .AsSingleton();
            container.Register<IBundlesCache<ScriptBundle>, BundlesCache<ScriptBundle>>()
                .AsSingleton();

            container.Register<IWebAssetResolverFactory, WebAssetResolverFactory>()
                .AsSingleton();

            container.Register<IWebAssetBundleCollectionResolver, WebAssetBundleCollectionResolver>()
                .AsSingleton();

            container.Register<IAssetLocator<FromDirectoryComponent>>((c, p) => new FromDirectoryAssetLocator(c.Resolve<HttpServerUtilityBase>(), HttpContext().Request.PhysicalApplicationPath));

            ConfigureContainerForStyleSheets();
            ConfigureContainerForScript();
            ConfigureHttpHandler();

        }

        public void ConfigureContainerForStyleSheets()
        {
            container.Register<IUrlGenerator<StyleSheetBundle>, StyleSheetUrlGenerator>();
            container.Register<IStyleSheetCompressor>((c, p) => DefaultSettings.StyleSheetCompressor);
            container.Register<IMergedBundleCache<StyleSheetBundle>, MergedBundleCache<StyleSheetBundle>>();
            container.Register<IStyleSheetConfigProvider>((c, p) => DefaultSettings.StyleSheetConfigProvider);
        }

        public void ConfigureContainerForScript()
        {
            container.Register<IScriptCompressor>((c, p) => DefaultSettings.ScriptCompressor);
            container.Register<IMergedBundleCache<ScriptBundle>, MergedBundleCache<ScriptBundle>>();
            container.Register<IScriptConfigProvider>((c, p) => DefaultSettings.ScriptConfigProvider);
        }

        public void ConfigureHttpHandler()
        {
            container.Register<HttpHandlerFactory>()
                .AsSingleton();
            container.Register<EncoderFactory>()
                .AsSingleton();
        }

        private HttpContextBase HttpContext()
        {
            return new HttpContextWrapper(System.Web.HttpContext.Current);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}