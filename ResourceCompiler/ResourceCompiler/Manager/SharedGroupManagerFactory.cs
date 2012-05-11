

namespace ResourceCompiler.Web.Mvc
{
    public class SharedGroupManagerFactory : ISharedGroupManagerFactory
    {
        private SharedGroupManager manager;
        private ISharedGroupConfigurationLoader mapper;

        public SharedGroupManagerFactory(ISharedGroupConfigurationLoader mapper)
        {
            this.mapper = mapper;
        }

        public SharedGroupManager Create()
        {
            if (manager == null)
            {
                manager = new SharedGroupManager();

                mapper.LoadScripts(manager.Scripts);
                mapper.LoadStyleSheets(manager.StyleSheets);
            }

            return manager;
        }
    }
}
