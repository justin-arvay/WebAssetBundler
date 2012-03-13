
namespace ResourceCompiler
{
    using ResourceCompiler.Registrar;
    using System.Web.Mvc;
    using ResourceCompiler.WebAsset;
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
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver();
            var resolverFactory = new WebAssetResolverFactory();
            var resolver = new WebAssetGroupCollectionResolver(urlResolver, resolverFactory);

            return new StyleSheetRegistrarBuilder(new StyleSheetRegistrar(collection), viewContext, resolver);
        }
    }
}
