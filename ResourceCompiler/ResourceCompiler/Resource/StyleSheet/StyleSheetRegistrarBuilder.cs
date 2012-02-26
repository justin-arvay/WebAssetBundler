using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace ResourceCompiler.Resource.StyleSheet
{
    public class StyleSheetRegistrarBuilder : IHtmlString
    {
        private readonly StyleSheetRegistrar registrar;

        public StyleSheetRegistrarBuilder(StyleSheetRegistrar registrar)
        {
            this.registrar = registrar;
        }

        public StyleSheetRegistrarBuilder DefaultGroup(Action<ResourceGroupBuilder> action)
        {
            action(new ResourceGroupBuilder(registrar.DefaultGroup));
            return this;
        }

        public void Render()
        {

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
