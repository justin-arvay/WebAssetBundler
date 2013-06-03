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
    using System.Web;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BundlerBase<TBundle> where TBundle : Bundle
    {
        public BundlerBase(IBundleProvider<TBundle> provider, IBundleRenderer<TBundle> renderer,
            IBundleDependencyResolver<TBundle> resolver)
        {
            Provider = provider;
            Renderer = renderer;
            Resolver = resolver;
        }

        internal BundlerState State
        {
            get;
            set;
        }

        protected IBundleRenderer<TBundle> Renderer
        {
            get;
            private set;
        }

        protected IBundleProvider<TBundle> Provider
        {
            get;
            private set;
        }

        protected IBundleDependencyResolver<TBundle> Resolver
        {
            get;
            private set;
        }

        /// <summary>
        /// References a bundle to be rendered when RendereReferences is called.
        /// </summary>
        /// <param name="bundleName"></param>
        public void Reference(string bundleName)
        {
            State.AddReference(bundleName);
        }


        /// <summary>
        /// Renders all referenced bundles including any bundles that are required by the referenced bundles.
        /// </summary>
        /// <returns></returns>
        public IHtmlString RenderReferenced()
        {
            if (State.IsReferencedRendered)
            {
                throw new InvalidOperationException(TextResource.Exceptions.RenderReferencedCalledTooManyTimes);
            }

            State.IsReferencedRendered = true;

            IEnumerable<TBundle> bundles = GetReferencedBundles(State);
            bundles = Resolver.ResolveReferenced(bundles);

            return Renderer.RenderAll(bundles, State);
        }

        /// <summary>
        /// Gets the bundle from either a url or a virtual path depending on the source passed.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected TBundle GetBundleBySource(string source)
        {
            TBundle bundle = null;

            if (source.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                bundle = Provider.GetExternalBundle(source);
            }
            else
            {
                bundle = Provider.GetSourceBundle(source);
            }

            return bundle;
        }

        protected IEnumerable<TBundle> GetReferencedBundles(BundlerState state)
        {
            var bundles = new List<TBundle>();

            foreach (string name in state.ReferencedBundleNames)
            {
                bundles.Add(Provider.GetNamedBundle(name));
            }

            return bundles;
        }        
    }
}
