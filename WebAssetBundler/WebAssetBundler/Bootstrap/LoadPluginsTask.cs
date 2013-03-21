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
    using System.Collections.Generic;
    using System.Linq;

    [TaskOrder(2)]
    public class LoadPluginsTask : IBootstrapTask
    {
        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            LoadPlugins<ScriptBundle>(container, typeProvider);
            LoadPlugins<StyleSheetBundle>(container, typeProvider);
        }

        /// <summary>
        /// Loads the plugins for the specified bundle type.
        /// </summary>
        /// <typeparam name="TBundle"></typeparam>
        /// <param name="container"></param>
        /// <param name="typeProvider"></param>
        public void LoadPlugins<TBundle>(TinyIoCContainer container, ITypeProvider typeProvider)
            where TBundle : Bundle
        {
            var scriptPlugins = typeProvider.GetImplementationTypes(typeof(IPluginConfiguration<TBundle>));

            foreach (var pluginType in scriptPlugins)
            {
                var plugin = (IPluginConfiguration<TBundle>)container.Resolve(pluginType);
                plugin.Configure(container);
            }
        }

        /*
        private IEnumerable<Type> GetPluginTypes(TinyIoCContainer container, IEnumerable<Type> allTypes)
        {
            var plugins =
                from type in allTypes
                from interfaceType in type.GetInterfaces()
                where interfaceType.IsGenericType &&
                      interfaceType.GetGenericTypeDefinition() == typeof(IPluginConfiguration<>)
                select type;

            return plugins;          
        }
        */
    }
}
