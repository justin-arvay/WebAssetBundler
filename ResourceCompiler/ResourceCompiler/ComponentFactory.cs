
namespace ResourceCompiler
{   
    using ResourceCompiler.Resource.StyleSheet;
    using System.Web.Mvc;

    public class ComponentFactory
    {
        private static readonly object styleSheetSyncLock = new object();
        private static StyleSheetRegistrarBuilder styleSheetRegistrarBuilder;

        private ViewContext viewContext;

        public ComponentFactory(ViewContext viewContext)
        {
            this.viewContext = viewContext;
        }

        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {

            if (styleSheetRegistrarBuilder == null)
            {
                lock (styleSheetSyncLock)
                {
                    if (styleSheetRegistrarBuilder == null)
                    {
                        styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(), viewContext);
                    }
                }
            }

            return styleSheetRegistrarBuilder;
        }
    }
}
