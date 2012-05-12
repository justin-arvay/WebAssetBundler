

namespace ResourceCompiler.Web.Mvc
{
    public class SharedWebAssetGroupFactory : ISharedWebAssetGroupFactory
    {
        public WebAssetGroup Create(GroupConfigurationElementCollection collection)
        {
            return new WebAssetGroup(collection.Name, true, DefaultSettings.GeneratedFilesPath)
            { 
                Combine = collection.Combine,
                Compress = collection.Compress,
                Version = collection.Version
            };
        }
    }
}
