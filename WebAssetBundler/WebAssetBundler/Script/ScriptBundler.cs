// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Web.UI;
    using System.Web;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using WebAssetBundler.TextResource;
    using WebAssetBundler.Web.Mvc;
    using System.Collections.Generic;

    public class ScriptBundler : BundlerBase<ScriptBundle>
    {

        public ScriptBundler(IBundleProvider<ScriptBundle> bundleProvider, IBundleRenderer<ScriptBundle> renderer,
            IBundleDependencyResolver<ScriptBundle> resolver)
            : base(bundleProvider, renderer, resolver)
        {
        }

        /// <summary>
        /// Renders the script into the responce stream.
        /// </summary>
        public IHtmlString Render(string name)
        {
            ScriptBundle bundle = Provider.GetNamedBundle(name);

            IEnumerable<ScriptBundle> bundles = Resolver.Resolve(bundle);

            return Renderer.RenderAll(bundles, State);           
        }

        /// <summary>
        ///  Renders the script into the responce stream.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IHtmlString Render(string name, Action<ScriptTagBuilder> builder)
        {
            ScriptBundle bundle = Provider.GetNamedBundle(name);

            builder(new ScriptTagBuilder(bundle));

            IEnumerable<ScriptBundle> bundles = Resolver.Resolve(bundle);

            return Renderer.RenderAll(bundles, State);           
        }

        /// <summary>
        /// Renders and included asset or external script bundle into the response stream.
        /// </summary>
        public IHtmlString Include(string source)
        {
            ScriptBundle bundle = GetBundleBySource(source);

            IEnumerable<ScriptBundle> bundles = Resolver.Resolve(bundle);

            return Renderer.RenderAll(bundles, State);           
        }

        /// <summary>
        /// Renders and included asset or external script bundle into the response stream.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IHtmlString Include(string source, Action<ScriptTagBuilder> builder)
        {
            ScriptBundle bundle = GetBundleBySource(source);

            builder(new ScriptTagBuilder(bundle));

            IEnumerable<ScriptBundle> bundles = Resolver.Resolve(bundle);

            return Renderer.RenderAll(bundles, State);           
        }
    }
}
