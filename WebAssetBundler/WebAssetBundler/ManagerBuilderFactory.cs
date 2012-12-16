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

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web;

    public class ManagerBuilderFactory
    {
        private HttpContextBase httpContext;
        private ICacheProvider cacheProvider;
        private IBuilderContextFactory contextFactory;

        public ManagerBuilderFactory(HttpContextBase httpContext, ICacheProvider cacheProvider, IBuilderContextFactory contextFactory)
        {
            this.httpContext = httpContext;
            this.cacheProvider = cacheProvider;
            this.contextFactory = contextFactory;
        }

        public StyleSheetBundler CreateStyleSheetManagerBuilder()
        {
            var container = WabHttpModule.Host.Container;            
            var builderContext = contextFactory.CreateStyleSheetContext();
            var bundleProvider = container.Resolve<StyleSheetBundleProvider>();          

            var urlGenerator = new StyleSheetUrlGenerator();
            var pipeline = container.Resolve<StyleSheetPipeline>();    
            var tagWriter = new StyleSheetTagWriter(urlGenerator);

            return new StyleSheetBundler(
                container.Resolve<IBundleProvider<StyleSheetBundle>>(),
                tagWriter,         
                builderContext);
        }

        public ScriptBundler CreateScriptManagerBuilder()
        {
            var container = WabHttpModule.Host.Container;
            var builderContext = contextFactory.CreateScriptContext();
            var bundleProvider = container.Resolve<ScriptBundleProvider>();

            var urlGenerator = new ScriptUrlGenerator();
            var pipeline = container.Resolve<ScriptPipeline>();         
            var tagWriter = new ScriptTagWriter(urlGenerator);

            return new ScriptBundler(
                container.Resolve<IBundleProvider<ScriptBundle>>(),
                tagWriter,              
                builderContext);
        }
    }
}
