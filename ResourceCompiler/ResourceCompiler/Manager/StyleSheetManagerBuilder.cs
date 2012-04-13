
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

    public class StyleSheetManagerBuilder : IHtmlString
    {
        private readonly IWebAssetGroupCollectionResolver collectionResolver;
        private bool hasRendered;
        private ViewContext viewContext;
        private ICacheFactory cacheFactory;        
        private IUrlResolver urlResolver;        
        private IWebAssetGenerator generator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="viewContext"></param>
        /// <param name="resolver"></param>
        public StyleSheetManagerBuilder(
            StyleSheetManager manager, 
            ViewContext viewContext, 
            IWebAssetGroupCollectionResolver resolver, 
            IUrlResolver urlResolver,            
            ICacheFactory cacheFactory,            
            IWebAssetGenerator generator)
        {
            Manager = manager;
            this.collectionResolver = resolver;
            this.urlResolver = urlResolver;
            this.viewContext = viewContext;
            this.cacheFactory = cacheFactory;                        
            this.generator = generator;
        }

        /// <summary>
        /// Allows building of the default group.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StyleSheetManagerBuilder DefaultGroup(Action<WebAssetGroupBuilder> action)
        {
            action(new WebAssetGroupBuilder(Manager.DefaultGroup));
            return this;
        }

        /// <summary>
        /// Allows building of stylesheets for the page.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StyleSheetManagerBuilder StyleSheets(Action<WebAssetGroupCollectionBuilder> action)
        {
            action(new WebAssetGroupCollectionBuilder(WebAssetType.StyleSheet, Manager.StyleSheets));
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
        public void Render()
        {
            if (hasRendered)
            {
                throw new InvalidOperationException(TextResource.Exceptions.YouCannotCallRenderMoreThanOnce);
            }

            var results = collectionResolver.Resolve(Manager.StyleSheets);
            var baseWriter = viewContext.Writer;

            generator.Generate(results);

            using (HtmlTextWriter textWriter = new HtmlTextWriter(baseWriter))
            {
                Write(textWriter, results);
            }

            hasRendered = true;
        }

        /// <summary>
        /// Returns the stylesheets as a string.
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            var results = collectionResolver.Resolve(Manager.StyleSheets);

            generator.Generate(results);

            using (var output = new StringWriter())
            {
                Write(output, results);

                return output.ToString();
            }
        }

        protected virtual void Write(TextWriter writer, IList<WebAssetResolverResult> results)
        {
            var link = "<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\"/>";            

            foreach (var result in results)
            {
                writer.WriteLine(link.FormatWith(urlResolver.Resolve(result.Path)));
            }
        }
    }
}