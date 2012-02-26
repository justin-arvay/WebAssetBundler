using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Resource.StyleSheet
{
    public class StyleSheetRegistrar
    {
        public StyleSheetRegistrar()
        {
            DefaultGroup = new ResourceGroup("Default", false)
            {
                DefaultPath = DefaultSettings.StyleSheetFilesPath
            };
        }

        public ResourceGroup DefaultGroup 
        { 
            get; 
            private set; 
        }
    }
}
