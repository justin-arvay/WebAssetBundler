
namespace ResourceCompiler
{   
    using ResourceCompiler.Resource.StyleSheet;
    using System.Web.Mvc;
    using ResourceCompiler.Resource;
    using ResourceCompiler.Resolvers;

    public class ComponentFactory
    {

        private ViewContext viewContext;

        public ComponentFactory(ViewContext viewContext)
        {
            this.viewContext = viewContext;
        }

        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {
            var collection = new ResourceGroupCollection();
            var urlResolver = new UrlResolver();
            var resolverFactory = new ResourceResolverFactory();
            var resolver = new ResourceGroupCollectionResolver(urlResolver, resolverFactory);

            return new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(collection), viewContext, resolver);
        }
    }
}
