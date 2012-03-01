using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Resource.StyleSheet;

namespace ResourceCompiler
{
    public class ComponentFactory
    {
        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {
            var registrar = new StyleSheetRegistrar();
            return new StyleSheetRegistrarBuilder(registrar);
        }
    }
}
