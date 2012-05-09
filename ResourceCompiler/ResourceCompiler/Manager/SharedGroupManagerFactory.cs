

namespace ResourceCompiler.Web.Mvc
{
    public class SharedGroupManagerFactory : ISharedGroupManagerFactory
    {
        private SharedGroupManager manager;

        private ISharedGroupConfigurationMapper mapper;

        public SharedGroupManagerFactory(ISharedGroupConfigurationMapper mapper)
        {
            this.mapper = mapper;
        }

        public SharedGroupManager Create()
        {
            if (manager == null)
            {
                manager = new SharedGroupManager();

                mapper.MapScripts(manager.Scripts);
                mapper.MapStyleSheets(manager.StyleSheets);
            }

            return manager;
        }
    }
}
