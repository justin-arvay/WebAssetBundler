
namespace ResourceCompiler
{   
    using ResourceCompiler.Resource.StyleSheet;
    using System.Web.Mvc;
    using ResourceCompiler.Resource;
    using ResourceCompiler.Resolvers;

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
                        var collection = new ResourceGroupCollection();
                        var urlResolver = new UrlResolver();
                        var resolverFactory = new ResourceResolverFactory();
                        var resolver = new ResourceGroupCollectionResolver(urlResolver, resolverFactory);

                        styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(collection), viewContext, resolver);
                    }
                }
            }

            return styleSheetRegistrarBuilder;
        }
    }
}
