
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
            var collectionResolver = new WebAssetGroupCollectionResolver(urlResolver, resolverFactory);
            var collectionMerger = new WebAssetGroupCollectionMerger(new WebAssetMergerFactory(new WebAssetReader()));
            var writer = new WebAssetMergerResultWriter("css", new PathResolver());

            return new StyleSheetRegistrarBuilder(
                new StyleSheetRegistrar(collection), 
                viewContext, 
                collectionResolver,
                collectionMerger,
                writer,
                cacheFactory);
        }
    }
}
