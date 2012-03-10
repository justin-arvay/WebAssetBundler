
namespace ResourceCompiler.Resource.StyleSheet
{
    using System.Web.UI;
    using System.Web;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using ResourceCompiler.TextResource;
    using ResourceCompiler.Resolvers;
    using ResourceCompiler.Extensions;

    public class StyleSheetRegistrarBuilder : IHtmlString
    {
        private readonly IResourceGroupCollectionResolver resolver;

        private bool hasRendered;

        private ViewContext viewContext;

        public StyleSheetRegistrarBuilder(StyleSheetRegistrar registrar, ViewContext viewContext, IResourceGroupCollectionResolver resolver)
        {
            Registrar = registrar;
            this.viewContext = viewContext;
        }

        public StyleSheetRegistrarBuilder DefaultGroup(Action<ResourceGroupBuilder> action)
        {
            action(new ResourceGroupBuilder(Registrar.DefaultGroup));
            return this;
        }

        public StyleSheetRegistrar Registrar 
        { 
            get; 
            private set; 
        }

        public void Render()
        {
            if (hasRendered)
            {
                throw new InvalidOperationException(TextResource.Exceptions.YouCannotCallRenderMoreThanOnce);
            }

            var baseWriter = viewContext.Writer;

            using (HtmlTextWriter textWriter = new HtmlTextWriter(baseWriter))
            {
                Write(baseWriter);
            }

            hasRendered = true;
        }

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
