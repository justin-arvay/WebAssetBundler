
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
            var pathResolver = new PathResolver();
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var collectionMerger = new WebAssetGroupCollectionMerger(new WebAssetMergerFactory(new WebAssetReader(viewContext.HttpContext.Server)));
            var writer = new WebAssetMergerResultWriter("css", pathResolver, new DirectoryWriter(), viewContext.HttpContext.Server);

            return new StyleSheetRegistrarBuilder(
                new StyleSheetRegistrar(collection), 
                viewContext, 
                collectionResolver,
                urlResolver,
                collectionMerger,
                writer,
                cacheFactory);
        }
    }
}
