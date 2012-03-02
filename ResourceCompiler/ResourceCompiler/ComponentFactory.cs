using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Resource.StyleSheet;

namespace ResourceCompiler
{
    public class ComponentFactory
    {
        private static readonly object styleSheetSyncLock = new object();
        private static StyleSheetRegistrarBuilder styleSheetRegistrarBuilder;

        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {

            if (styleSheetRegistrarBuilder == null)
            {
                lock (styleSheetSyncLock)
                {
                    if (styleSheetRegistrarBuilder == null)
                    {
                        styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(new StyleSheetRegistrar());
                    }
                }
            }

            return styleSheetRegistrarBuilder;
        }
    }
}
