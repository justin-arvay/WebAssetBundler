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

    public class StyleSheetManagerBuilder : IHtmlString
    {
        private readonly IWebAssetBundleCollectionResolver collectionResolver;
        private bool hasRendered;
        private ITagWriter<StyleSheetBundle> tagWriter;
        private BuilderContext context;
        private IBundlePipeline<StyleSheetBundle> pipeline;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="viewContext"></param>
        /// <param name="resolver"></param>
        public StyleSheetManagerBuilder(
            IBundlePipeline<StyleSheetBundle> pipeline,
            StyleSheetManager manager, 
            IWebAssetBundleCollectionResolver resolver,
            ITagWriter<StyleSheetBundle> tagWriter,                   
            BuilderContext context)
        {
            this.pipeline = pipeline;
            Manager = manager;
            this.collectionResolver = resolver;
            this.tagWriter = tagWriter;      
            this.context = context;
        }

        /// <summary>
        /// Allows building of stylesheets for the page.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StyleSheetManagerBuilder StyleSheets(Action<WebAssetBundleCollectionBuilder<StyleSheetBundle>> action)
        {
            action(new WebAssetBundleCollectionBuilder<StyleSheetBundle>(Manager.StyleSheetBundles, context));
            return this;
        }

        /// <summary>
        /// A manager of all the stylesheets. 
        /// </summary>
        public StyleSheetManager Manager 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Renders the stylesheets into the responce stream.
        /// </summary>
        public IHtmlString RenderAll()
        {
            if (hasRendered)
            {
                throw new InvalidOperationException(TextResource.Exceptions.YouCannotCallRenderMoreThanOnce);
            }

            var results = merger.Merge(collectionResolver.Resolve(Manager.StyleSheetBundles, context), context);

            using (HtmlTextWriter textWriter = new HtmlTextWriter(new StringWriter()))
            {
                tagWriter.Write(textWriter, results, context);
            }

            hasRendered = true;
        }

        /// <summary>
        /// Returns the stylesheets as a string.
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            var results = merger.Merge(collectionResolver.Resolve(Manager.StyleSheetBundles, context), context);

            using (var output = new StringWriter())
            {
                tagWriter.Write(output, results, context);

                return output.ToString();
            }
        }
    }
}