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
    using System.Web;
    using System.Collections.Generic;

    public abstract class ConfigureContainerTaskBase : IBootstrapTask
    {

        public virtual void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the settings context for the bundle type.
        /// </summary>
        /// <typeparam name="TBundle"></typeparam>
        /// <returns></returns>
        public SettingsContext<TBundle> CreateSettings<TBundle>() where TBundle : Bundle
        {
            return new SettingsContext<TBundle>(DefaultSettings.DebugMode, DefaultSettings.MinifyIdentifier);
        }


        /// <summary>
        /// Loads the plugins for the specified bundle type.
        /// </summary>
        /// <typeparam name="TBundle"></typeparam>
        /// <param name="container"></param>
        /// <param name="typeProvider"></param>
        public IEnumerable<IPlugin<TBundle>> LoadPlugins<TBundle>(TinyIoCContainer container, ITypeProvider typeProvider)
            where TBundle : Bundle
        {
            var scriptPlugins = typeProvider.GetImplementationTypes(typeof(IPlugin<TBundle>));
            var plugins = new List<IPlugin<TBundle>>();

            foreach (var pluginType in scriptPlugins)
            {
                plugins.Add((IPlugin<TBundle>)container.Resolve(pluginType));
            }

            return plugins;
        }
    }
}
