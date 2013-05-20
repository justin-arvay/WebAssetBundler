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


        /// <summary>
        /// Gets the bundle from either a url or a virtual path depending on the source passed.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public TBundle GetBundleBySource(string source)
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
        public IHtmlString WriteBundle(TBundle bundle)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                tagWriter.Write(textWriter, bundle);
                return new HtmlString(textWriter.ToString());
            }
        }
    }
}
