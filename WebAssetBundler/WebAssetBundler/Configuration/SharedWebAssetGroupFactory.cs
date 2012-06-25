

namespace WebAssetBundler.Web.Mvc
{
    public class SharedWebAssetGroupFactory : ISharedWebAssetGroupFactory
    {
        public WebAssetGroup Create(GroupConfigurationElementCollection collection)
        {
            return new WebAssetGroup(collection.Name, true)
            { 
                Combine = collection.Combine,
                Compress = collection.Compress,
                Version = collection.Version
            };
        }
    }
}
