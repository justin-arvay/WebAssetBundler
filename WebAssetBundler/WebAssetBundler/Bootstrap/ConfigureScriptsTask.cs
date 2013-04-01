// ResourceCompiler - Compiles web assets so you dont have to.
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

    [TaskOrder(3)]
    public class ConfigureScriptsTask : ConfigureContainerTaskBase<ScriptBundle>
    {
        public override void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var pipelineModifers = new List<IPipelineModifier<ScriptBundle>>();
            var searchPatterns = CreateDefaultPatterns();
            var plugins = LoadPlugins(container, typeProvider);

            foreach (var plugin in plugins)
            {
                plugin.Initialize(container);
                pipelineModifers.AddRange(GetPipelineModifiers(plugin));
                searchPatterns.AddRange(GetSearchPatterns(plugin));
            }

            ConfigureContainer(container, pipelineModifers);
            RegisterDirectorySearch<ScriptBundle>(container, searchPatterns);
        }

        public void ConfigureContainer(TinyIoCContainer container, IEnumerable<IPipelineModifier<ScriptBundle>> pipelineModifiers)
        {
            container.Register<IScriptMinifier>((c, p) => DefaultSettings.ScriptMinifier);
            container.Register<IBundlesCache<ScriptBundle>, BundlesCache<ScriptBundle>>();
            container.Register<IBundleConfigurationProvider<ScriptBundle>>((c, p) => DefaultSettings.ScriptConfigurationProvider(c));
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => CreateScriptPipeline(c, pipelineModifiers));
            container.Register<ITagWriter<ScriptBundle>, ScriptTagWriter>();
            container.Register<IBundleCachePrimer<ScriptBundle>, ScriptBundleCachePrimer>();
            container.Register<IBundleProvider<ScriptBundle>, ScriptBundleProvider>();
            container.Register<IDirectorySearchFactory<ScriptBundle>, DirectorySearchFactory<ScriptBundle>>();            
        }

        /// <summary>
        /// Creates the pipeline as well as modifies it using supplied modifiers.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pipelineModifiers"></param>
        /// <returns></returns>
        public IBundlePipeline<ScriptBundle> CreateScriptPipeline(TinyIoCContainer container, IEnumerable<IPipelineModifier<ScriptBundle>> pipelineModifiers)
        {
            var pipeline = new ScriptPipeline(container);

            ModifyPipeline(pipeline, pipelineModifiers);

            return pipeline;
        }

        /// <summary>
        /// Creates the default search patterns to be used in configuration by directory.
        /// </summary>
        /// <returns></returns>
        public List<string> CreateDefaultPatterns()
        {
            return new List<string>() { "*.js" };
        }
    }
}
