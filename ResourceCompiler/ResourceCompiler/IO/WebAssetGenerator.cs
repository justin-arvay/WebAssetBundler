
namespace ResourceCompiler.Web.Mvc
{
    using System.Collections.Generic;

    public class WebAssetGenerator : IWebAssetGenerator
    {
        private IWebAssetWriter writer;
        private IWebAssetMerger merger;

        public WebAssetGenerator(IWebAssetWriter writer, IWebAssetMerger merger)
        {
            this.writer = writer;
            this.merger = merger;
        }

        public void Generate(IList<WebAssetResolverResult> resolverResults)
        {
            foreach (var resolverResult in resolverResults)
            {
                writer.Write(merger.Merge(resolverResult));
            }
        }
    }
}
