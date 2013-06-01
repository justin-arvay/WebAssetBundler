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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class BundleDependencyResolver<TBundle> : IBundleDependencyResolver<TBundle>
        where TBundle : Bundle
    {

        private IBundleProvider<TBundle> provider;

        public BundleDependencyResolver(IBundleProvider<TBundle> provider)
        {
            this.provider = provider;
        }

        public IEnumerable<TBundle> Resolve(TBundle bundle)
        {
            var resolvedBundles = new List<TBundle>();
            resolvedBundles.Add(bundle);
            resolvedBundles.AddRange(GetRequiredBundles(bundle, 0));
            
            return PrepareBundles(resolvedBundles);
        }

        public IEnumerable<TBundle> ResolveReferenced(IEnumerable<TBundle> bundles)
        {
            var resolvedBundles = new List<TBundle>();

            foreach (var bundle in bundles)
            {
                resolvedBundles.Add(bundle);
                resolvedBundles.AddRange(GetRequiredBundles(bundle, 0));                
            }

            return PrepareBundles(resolvedBundles);        
        }

        /// <summary>
        /// Recursively gets all the required bundles specificed by a bundle. Cannot be deeper than 25.
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        protected ICollection<TBundle> GetRequiredBundles(TBundle bundle, int depth)
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
                reqBundle = provider.GetNamedBundle(name);

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
        protected IEnumerable<TBundle> PrepareBundles(IEnumerable<TBundle> bundles)
        {
            var sortedBundles = new List<TBundle>(bundles);
            sortedBundles.Reverse(); //reverse is needed for distinct

            //distinct will keep the first bundles it encounter and remove the later
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
