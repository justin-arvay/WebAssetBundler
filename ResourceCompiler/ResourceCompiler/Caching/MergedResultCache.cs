
namespace ResourceCompiler.Web.Mvc
{
    using System;

    public class MergedResultCache : IMergedResultCache
    {
        private ICacheProvider provider;
        private const string keyPrefix = "MergedResult->";
        private WebAssetType type;

        public MergedResultCache(WebAssetType type, ICacheProvider provider)
        {
            this.provider = provider;
            this.type = type;
        }

        /// <summary>
        /// Adds the result to the cache.
        /// </summary>
        /// <param name="result"></param>
        public void Add(WebAssetMergerResult result)
        {
            if (result.Path.IsNotNullOrEmpty())
            {
                provider.Insert(GetKey(result), result);
            }
            else
            {
                throw new InvalidOperationException("Result path is not supplied");
            }
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
            var typePrefix = "";

            switch (type)
            {
                case WebAssetType.Script:
                    typePrefix = "Script";
                    break;
                case WebAssetType.StyleSheet:
                    typePrefix = "StyleSheet";
                    break;
            }

            var keySuffix = result.Path
                .Replace("~", "")
                .Replace("\\", ".")
                .Replace("/", ".");

            return typePrefix + "->" + keyPrefix + keySuffix;
        }
    }
}
