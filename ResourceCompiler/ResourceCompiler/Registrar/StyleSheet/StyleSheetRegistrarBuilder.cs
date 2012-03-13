
namespace ResourceCompiler.Registrar
{
    using System.Web.UI;
    using System.Web;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using ResourceCompiler.TextResource;
    using ResourceCompiler.Resolvers;
    using ResourceCompiler.Extensions;
    using ResourceCompiler.WebAsset;

    public class StyleSheetRegistrarBuilder : IHtmlString
    {
        private readonly IWebAssetGroupCollectionResolver resolver;

        private bool hasRendered;

        private ViewContext viewContext;

        public StyleSheetRegistrarBuilder(StyleSheetRegistrar registrar, ViewContext viewContext, IWebAssetGroupCollectionResolver resolver)
        {
            Registrar = registrar;
            this.resolver = resolver;
            this.viewContext = viewContext;
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
        /// Allows building of groups and page.
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
           using (var output = new StringWriter())
           {
               Write(output);

               return output.ToString();
           }
        }

        protected virtual void Write(TextWriter writer)
        {
            var link = "<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\"/>";
            var urls = resolver.Resolve(Registrar.StyleSheets);

            foreach (var url in urls)
            {
                writer.WriteLine(link.FormatWith(url));
            }
        }
    }
}
