// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc
{
    using System.Web.UI;
    using System.Web;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using ResourceCompiler.TextResource;
    using ResourceCompiler.Web.Mvc;
    using System.Collections.Generic;

    public class ScriptManagerBuilder : IHtmlString
    {
        private readonly IWebAssetGroupCollectionResolver collectionResolver;
        private bool hasRendered;
        private ViewContext viewContext;    
        private ITagWriter tagWriter;
        private IWebAssetGenerator generator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="viewContext"></param>
        /// <param name="resolver"></param>
        public ScriptManagerBuilder(
            ScriptManager manager, 
            ViewContext viewContext, 
            IWebAssetGroupCollectionResolver resolver,
            ITagWriter tagWriter,
            IWebAssetGenerator generator)
        {
            Manager = manager;
            this.collectionResolver = resolver;
            this.tagWriter = tagWriter;
            this.viewContext = viewContext;
            this.generator = generator;
        }

        /// <summary>
        /// Allows building of the default group.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ScriptManagerBuilder DefaultGroup(Action<WebAssetGroupBuilder> action)
        {
            action(new WebAssetGroupBuilder(Manager.DefaultGroup));
            return this;
        }

        /// <summary>
        /// Allows building of stylesheets for the page.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ScriptManagerBuilder Scripts(Action<WebAssetGroupCollectionBuilder> action)
        {
            action(new WebAssetGroupCollectionBuilder(WebAssetType.Script, Manager.Scripts, DefaultSettings.GeneratedFilesPath));
            return this;
        }

        /// <summary>
        /// A manager of all the stylesheets. 
        /// </summary>
        public ScriptManager Manager 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Renders the stylesheets into the responce stream.
        /// </summary>
        public void Render()
        {
            if (hasRendered)
            {
                throw new InvalidOperationException(TextResource.Exceptions.YouCannotCallRenderMoreThanOnce);
            }

            var results = collectionResolver.Resolve(Manager.Scripts);
            var baseWriter = viewContext.Writer;

            generator.Generate(results);

            using (HtmlTextWriter textWriter = new HtmlTextWriter(baseWriter))
            {
                tagWriter.Write(textWriter, results);
            }

            hasRendered = true;
        }

        /// <summary>
        /// Returns the stylesheets as a string.
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            var results = collectionResolver.Resolve(Manager.Scripts);

            generator.Generate(results);

            using (var output = new StringWriter())
            {
                tagWriter.Write(output, results);

                return output.ToString();
            }
        }
    }
}
