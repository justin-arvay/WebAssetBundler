
namespace ResourceCompiler
{
    using System.Web.Mvc;
    using ResourceCompiler.Web.Mvc;

    public class ComponentFactory
    {

        private ViewContext viewContext;

        private ICacheFactory cacheFactory;

        public ComponentFactory(ViewContext viewContext)
        {
            this.viewContext = viewContext;
        }

        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory();
            var resolver = new WebAssetGroupCollectionResolver(urlResolver, resolverFactory);

            return new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(collection), viewContext, resolver, cacheFactory);
        }
    }
}
