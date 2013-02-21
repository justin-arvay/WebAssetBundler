// Web Asset Bundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Compilation;

    public class WebHost : IDisposable
    {
        private TinyIoCContainer container;
        private TinyIoCContainer childContainer;
        private Type[] allTypes;
        private TypeProvider typeProvider;


        public WebHost()
        {
            typeProvider = new TypeProvider(LoadAssemblies());
            allTypes = typeProvider.GetAllTypes();
            container = new TinyIoCContainer();
            childContainer = container.GetChildContainer();
        }

        public TinyIoCContainer Container
        {
            get
            {
                return container;
            }
        }

        public void RunBootstrapTasks()
        {
            childContainer.RegisterMultiple<IBootstrapTask>(typeProvider.GetImplementationTypes(typeof(IBootstrapTask)));
            var tasks = childContainer.ResolveAll<IBootstrapTask>();
            //TODO:: dispose of the child container using HttpApplication events

            foreach (var task in tasks)
            {
                task.StartUp(container, typeProvider);
            }
        }
        /*
        protected virtual IEnumerable<Type> GetConfigurationTypes(IEnumerable<Type> typesToSearch)
        {
            var publicTypes =
                from type in typesToSearch
                where type.IsClass && !type.IsAbstract
                from interfaceType in type.GetInterfaces()
                where interfaceType.IsGenericType &&
                      interfaceType.GetGenericTypeDefinition() == typeof(IConfiguration<>)
                select type;

            var internalTypes = new[]
            {
                typeof(ScriptContainerConfiguration),
                typeof(StylesheetsContainerConfiguration),
                typeof(HtmlTemplatesContainerConfiguration),
                typeof(SettingsVersionAssigner)
            };

            return publicTypes.Concat(internalTypes);
        }*/

        private  IEnumerable<Assembly> LoadAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToArray();
        }

        public void Dispose()
        {
            childContainer.Dispose();
            container.Dispose();
        }
    }
}
