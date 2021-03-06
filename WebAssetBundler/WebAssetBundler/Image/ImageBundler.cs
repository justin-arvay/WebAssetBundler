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
    using System.IO;

    public class ImageBundler
    {
        private IBundleProvider<ImageBundle> bundleProvider;
        private ITagWriter<ImageBundle> tagWriter;

        public ImageBundler(IBundleProvider<ImageBundle> bundleProvider, ITagWriter<ImageBundle> tagWriter)
        {
            this.bundleProvider = bundleProvider;
            this.tagWriter = tagWriter;
        }

        /// <summary>
        /// Renders the image into the responce stream.
        /// </summary>
        public IHtmlString Render(string source)
        {
            var bundle = bundleProvider.GetSourceBundle(source);

            using (StringWriter textWriter = new StringWriter())
            {
                tagWriter.Write(textWriter, bundle);
                return new HtmlString(textWriter.ToString());
            }
        }

        public IHtmlString Render(string source, Action<ImageTagBuilder> builder)
        {
            var bundle = bundleProvider.GetSourceBundle(source);

            builder(new ImageTagBuilder(bundle));

            using (StringWriter textWriter = new StringWriter())
            {
                tagWriter.Write(textWriter, bundle);
                return new HtmlString(textWriter.ToString());
            }
        }
    }
}
