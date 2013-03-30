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

            GetBootstrapTasks().ToList().ForEach(task =>
            {
                task.StartUp(container, typeProvider);
                task.ShutDown();
            });
        }

        /// <summary>
        /// Gets the bootstrap tasks in correct order.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IBootstrapTask> GetBootstrapTasks()
        {
            childContainer.RegisterMultiple<IBootstrapTask>(typeProvider.GetImplementationTypes(typeof(IBootstrapTask)));
            var tasks = childContainer.ResolveAll<IBootstrapTask>()
                .OrderBy((t) =>
                {
                    TaskOrderAttribute attribute = (TaskOrderAttribute)t.GetType().GetCustomAttributes(typeof(TaskOrderAttribute), true)
                        .SingleOrDefault();

                    return attribute != null ? attribute.Order : int.MaxValue;
                });


            //TODO:: dispose of the child container using HttpApplication events
            childContainer.Dispose();

            return tasks;
        }

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
