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
        protected ITagWriter<TBundle> tagWriter;
        protected IBundleProvider<TBundle> bundleProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="viewContext"></param>
        /// <param name="resolver"></param>
        public BundlerBase(
            IBundleProvider<TBundle> bundleProvider,
            ITagWriter<TBundle> tagWriter)
        {
            this.bundleProvider = bundleProvider;
            this.tagWriter = tagWriter;
        }

        internal BundlerState State
        {
            get;
            set;
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
            if (State.Rendered)
            {
                throw new InvalidOperationException(TextResource.Exceptions.RenderReferencedCalledTooManyTimes);
            }

            State.Rendered = true;

            IEnumerable<TBundle> bundles = GetReferencedBundles(State);

            return WriteBundles(bundles);
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
                bundle = bundleProvider.GetExternalBundle(source);
            }
            else
            {
                bundle = bundleProvider.GetSourceBundle(source);
            }

            return bundle;
        }

        /// <summary>
        /// Writes the bundle to an HtmlString
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        protected IHtmlString WriteBundle(TBundle bundle)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                if (State.IsRendered(bundle) == false)
                {
                    State.MarkRendered(bundle);
                    tagWriter.Write(textWriter, bundle);
                }

                return new HtmlString(textWriter.ToString());
            }
        }

        /// <summary>
        /// Writes all bundles to an HtmlString
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        protected IHtmlString WriteBundles(IEnumerable<TBundle> bundles)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                foreach (var bundle in bundles)
                {
                    if (State.IsRendered(bundle) == false)
                    {
                        State.MarkRendered(bundle);
                        tagWriter.Write(textWriter, bundle);
                    }
                }
                
                return new HtmlString(textWriter.ToString());
            }
        }

        protected IEnumerable<TBundle> GetReferencedBundles(BundlerState state)
        {
            var bundles = new List<TBundle>();

            foreach (string name in state.ReferencedBundleNames)
            {
                bundles.Add(bundleProvider.GetNamedBundle(name));
            }

            return bundles;
        }

        /// <summary>
        /// Recursively gets all the required bundles specificed by a bundle. Cannot be deeper than 25.
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        protected ICollection<TBundle> GetRequiredBundles(TBundle bundle,  int depth)
        {
            var bundles = new List<TBundle>();
            TBundle reqBundle = null;

            //assume anything deeper than 25 is a mistake.
            //That is a large amount of bundles that require eachother. 
            //At this point it is probably too difficult to keep track of bundle requirements.
            //Safe guards against circular reference through bundle names in the configuration.
            if (depth > 25)
            {
                throw new InvalidDataException();
            }

            foreach (var name in bundle.Required)
            {
                reqBundle = bundleProvider.GetNamedBundle(name);

                depth++;  //add to depth because we are about to go deeper

                bundles.Add(reqBundle);
                bundles.AddRange(GetRequiredBundles(reqBundle, depth));

                depth--; //we came up one level
            }

            return bundles;
        }

        /// <summary>
        /// Ensures the required bundles are in the correct order for writing the tags. Additionally removes duplicate bundles.
        /// Correct bundle order should always put the bundles with the more dependancy first, bundles that are the least dependant last.
        /// </summary>
        /// <param name="bundles"></param>
        /// <returns></returns>
        protected IEnumerable<TBundle> CorrectBundleOrder(IEnumerable<TBundle> bundles)
        {
            var sortedBundles = new List<TBundle>(bundles);
            sortedBundles.Reverse(); //reverse is needed for distinct

            //distince will keep the first bundles it encounter and remove the later
            //reversing before distinct will ensure that the required bundles are always first for bundlers that appear later in the list
            sortedBundles = sortedBundles.Distinct(new BundleComparer()).ToList(); 

            return sortedBundles;
        }

        private class BundleComparer : IEqualityComparer<TBundle>
        {

            public bool Equals(TBundle x, TBundle y)
            {
                return x.Name.IsCaseInsensitiveEqual(y.Name);
            }

            public int GetHashCode(TBundle obj)
            {
                return obj.Name.GetHashCode();
            }
        }
    }
}
