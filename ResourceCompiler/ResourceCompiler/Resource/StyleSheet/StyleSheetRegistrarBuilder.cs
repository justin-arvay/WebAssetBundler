
namespace ResourceCompiler.Resource.StyleSheet
{
    using System.Web.UI;
    using System.Web;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using ResourceCompiler.TextResource;

    public class StyleSheetRegistrarBuilder : IHtmlString
    {
        private readonly StyleSheetRegistrar registrar;

        private bool hasRendered;

        private ViewContext viewContext;

        public StyleSheetRegistrarBuilder(StyleSheetRegistrar registrar, ViewContext viewContext)
        {
            this.registrar = registrar;
            this.viewContext = viewContext;
        }

        public StyleSheetRegistrarBuilder DefaultGroup(Action<ResourceGroupBuilder> action)
        {
            action(new ResourceGroupBuilder(registrar.DefaultGroup));
            return this;
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

        }
    }
}
