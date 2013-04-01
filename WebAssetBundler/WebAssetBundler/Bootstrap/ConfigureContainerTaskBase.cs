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
    using System.Linq;

    public abstract class ConfigureContainerTaskBase<TBundle> : IBootstrapTask
        where TBundle : Bundle
    {
        public IEnumerable<IPlugin<TBundle>> Plugins
        {
            get;
            set;
        }

        public virtual void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the plugins for the specified bundle type.
        /// </summary>
        /// <typeparam name="TBundle"></typeparam>
        /// <param name="container"></param>
        /// <param name="typeProvider"></param>
        public IEnumerable<IPlugin<TBundle>> LoadPlugins(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var pluginTypes = typeProvider.GetImplementationTypes(typeof(IPlugin<TBundle>));
            var plugins = new List<IPlugin<TBundle>>();

            foreach (var pluginType in pluginTypes)
            {
                plugins.Add((IPlugin<TBundle>)container.Resolve(pluginType));
            }

            Plugins = plugins;

            return plugins;
        }

        public IEnumerable<IPipelineModifier<TBundle>> GetPipelineModifiers(IPlugin<TBundle> plugin)
        {
            var modifiers = new List<IPipelineModifier<TBundle>>();

            plugin.AddPipelineModifers(modifiers);

            return modifiers;
        }

        public IEnumerable<string> GetSearchPatterns(IPlugin<TBundle> plugin)
        {
            var patterns = new List<string>();

            plugin.AddSearchPatterns(patterns);

            return patterns;
        }

        /// <summary>
        /// Modifies the pipeline using the provided modifiers.
        /// </summary>
        /// <typeparam name="TBundle"></typeparam>
        /// <param name="pipeline"></param>
        /// <param name="pipelineModifiers"></param>
        public void ModifyPipeline(IBundlePipeline<TBundle> pipeline, IEnumerable<IPipelineModifier<TBundle>> pipelineModifiers)
        {
            foreach (var modifier in pipelineModifiers)
            {
                modifier.Modify(pipeline);
            }
        }

        /// <summary>
        /// Registers the directory search with the container. Specific to bundle type.
        /// </summary>
        /// <typeparam name="TBundle"></typeparam>
        /// <param name="container"></param>
        /// <param name="searchPatterns"></param>
        public void RegisterDirectorySearch<TBundle>(TinyIoCContainer container, IEnumerable<string> searchPatterns)
            where TBundle : Bundle
        {
            container.Register<DirectorySearch>((c, p) =>
            {
                return new DirectorySearch()
                {
                    Patterns = searchPatterns
                };
            }, DirectorySearch.GetDirectorySearchName<TBundle>());
        }

        public void ShutDown()
        {
            if (Plugins != null)
            {
                Plugins.ToList().ForEach(p => p.Dispose());
            }
        }
    }
}
