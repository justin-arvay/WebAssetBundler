
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

        public StyleSheetManagerBuilder StyleSheetManager()
        {
            var pathResolver = new PathResolver(WebAssetType.StyleSheet);
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new StyleSheetWebAssetMerger(new WebAssetReader(viewContext.HttpContext.Server), new ImagePathContentFilter(), viewContext.HttpContext.Server);

            return new StyleSheetManagerBuilder(
                new StyleSheetManager(collection), 
                viewContext, 
                collectionResolver,
                urlResolver,                
                writer,
                cacheFactory,
                merger);
        }

        public ScriptManagerBuilder ScriptManager()
        {
            var pathResolver = new PathResolver(WebAssetType.Javascript);
            var collection = new WebAssetGroupCollection();
            var urlResolver = new UrlResolver(viewContext.RequestContext);
            var resolverFactory = new WebAssetResolverFactory(pathResolver);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var writer = new WebAssetWriter(new DirectoryWriter(), viewContext.HttpContext.Server);
            var merger = new ScriptWebAssetMerger(new WebAssetReader(viewContext.HttpContext.Server));

            return new ScriptManagerBuilder(
                new ScriptManager(collection),
                viewContext,
                collectionResolver,
                urlResolver,
                writer,
                cacheFactory,
                merger);
        }
    }
}
