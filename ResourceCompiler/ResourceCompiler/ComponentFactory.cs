
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
            var pathResolver = new PathResolver(WebAssetType.StyleSheet);
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new StyleSheetWebAssetMerger(new WebAssetReader(viewContext.HttpContext.Server), new ImagePathContentFilter(), viewContext.HttpContext.Server);

            return new StyleSheetRegistrarBuilder(
                new StyleSheetRegistrar(collection), 
                viewContext, 
                collectionResolver,
                urlResolver,                
                writer,
                cacheFactory,
                merger);
        }
    }
}
