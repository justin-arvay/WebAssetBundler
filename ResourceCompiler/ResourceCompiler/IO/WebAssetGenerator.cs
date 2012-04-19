
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
                //check if the merger result exists in the cache
                //if exists:
                //do nothing
                //if not exists:
                //write the file
                //add to cache, cache provider needs seperate instance for stylesheet and sript, best way to do this would be a namespace
                writer.Write(merger.Merge(resolverResult));
            }
        }
    }
}
