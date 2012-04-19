
namespace ResourceCompiler.Web.Mvc
{
    public class MergedResultCache : IMergedResultCache
    {
        private ICacheProvider provider;
        private const string keyPrefix = "MergedResult->";

        public MergedResultCache(ICacheProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Adds the result to the cache.
        /// </summary>
        /// <param name="result"></param>
        public void Add(WebAssetMergerResult result)
        {
            provider.Insert(GetKey(result), result);
        }

        /// <summary>
        /// Checks if the result has already been cached.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Exists(WebAssetMergerResult result)
        {
            if (provider.Get(GetKey(result)) == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the key for the given result.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string GetKey(WebAssetMergerResult result)
        {
            var keySuffix = result.Path
                .Replace("~", "")
                .Replace("\\", ".")
                .Replace("/", ".");

            return keyPrefix + keySuffix;
        }
    }
}
