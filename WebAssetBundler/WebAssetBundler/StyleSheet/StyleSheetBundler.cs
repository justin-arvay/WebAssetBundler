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

    public class StyleSheetBundler
    {
        private ITagWriter<StyleSheetBundle> tagWriter;
        private IBundleProvider<StyleSheetBundle> bundleProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="viewContext"></param>
        /// <param name="resolver"></param>
        public StyleSheetBundler(
            IBundleProvider<StyleSheetBundle> bundleProvider,
            ITagWriter<StyleSheetBundle> tagWriter)
        {
            this.bundleProvider = bundleProvider;
            this.tagWriter = tagWriter;
        }

        /// <summary>
        /// Renders the stylesheets into the responce stream.
        /// </summary>
        public IHtmlString Render(string name)
        {
            var bundle = bundleProvider.GetNamedBundle(name);

            using (StringWriter textWriter = new StringWriter())
            {
                tagWriter.Write(textWriter, bundle);
                return new HtmlString(textWriter.ToString());
            }
        }

        public IHtmlString Include(string source)
        {
            StyleSheetBundle bundle = null;

            if (source.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                bundle = bundleProvider.GetExternalBundle(source);
            }
            else
            {
                bundle = bundleProvider.GetSourceBundle(source);
            }

            using (StringWriter textWriter = new StringWriter())
            {
                tagWriter.Write(textWriter, bundle);
                return new HtmlString(textWriter.ToString());
            } 
        }
    }
}