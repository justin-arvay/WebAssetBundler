
namespace ResourceCompiler.Web.Mvc
{

    public interface IMergedResultCache
    {
        void Add(WebAssetMergerResult result);
        bool Exists(WebAssetMergerResult result);
    }
}
