
namespace ResourceCompiler.Web.Mvc
{
    using System.Web.UI;
    using System.Web;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using ResourceCompiler.TextResource;
    using ResourceCompiler.Web.Mvc;

    public class StyleSheetRegistrarBuilder : IHtmlString
    {
        private readonly IWebAssetGroupCollectionResolver collectionResolver;
        private readonly IWebAssetGroupCollectionMerger collectionMerger;
        private bool hasRendered;
        private ViewContext viewContext;
        private ICacheFactory cacheFactory;
        private IWebAssetMergerResultWriter writer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="viewContext"></param>
        /// <param name="resolver"></param>
        public StyleSheetRegistrarBuilder(
            StyleSheetRegistrar registrar, 
            ViewContext viewContext, 
            IWebAssetGroupCollectionResolver resolver, 
            IWebAssetGroupCollectionMerger collectionMerger,
            IWebAssetMergerResultWriter writer,
            ICacheFactory cacheFactory)
        {
            Registrar = registrar;
            this.collectionResolver = resolver;
            this.viewContext = viewContext;
            this.cacheFactory = cacheFactory;
            this.collectionMerger = collectionMerger; 
            this.writer = writer;
        }

        /// <summary>
        /// Allows building of the default group.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StyleSheetRegistrarBuilder DefaultGroup(Action<WebAssetGroupBuilder> action)
        {
            action(new WebAssetGroupBuilder(Registrar.DefaultGroup));
            return this;
        }

        /// <summary>
        /// Allows building of stylesheets for the page.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StyleSheetRegistrarBuilder StyleSheets(Action<WebAssetGroupCollectionBuilder> action)
        {
            action(new WebAssetGroupCollectionBuilder(WebAssetType.StyleSheet, Registrar.StyleSheets));
            return this;
        }

        /// <summary>
        /// A registrar of all the stylesheets. 
        /// </summary>
        public StyleSheetRegistrar Registrar 
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

            var baseWriter = viewContext.Writer;

            Generate();

            using (HtmlTextWriter textWriter = new HtmlTextWriter(baseWriter))
            {
                Write(textWriter);
            }

            hasRendered = true;
        }

        /// <summary>
        /// Returns the stylesheets as a string.
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            Generate();

            using (var output = new StringWriter())
            {
                Write(output);

                return output.ToString();
            }
        }

        protected virtual void Write(TextWriter writer)
        {
            var link = "<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\"/>";
            var urls = collectionResolver.Resolve(Registrar.StyleSheets);

            foreach (var url in urls)
            {
                writer.WriteLine(link.FormatWith(url));
            }
        }

        protected virtual void Generate()
        {
            var results = collectionMerger.Merge(Registrar.StyleSheets);

            foreach (var result in results)
            {
                writer.Write(DefaultSettings.GeneratedFilesPath, result);
            }
        }
    }
}
