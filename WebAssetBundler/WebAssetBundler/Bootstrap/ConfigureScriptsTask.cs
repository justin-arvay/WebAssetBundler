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
    using System.Linq;

    [TaskOrder(3)]
    public class ConfigureScriptsTask : IBootstrapTask
    {
        private IPluginLoader pluginLoader;

        public ConfigureScriptsTask(IPluginLoader pluginLoader)
        {
            this.pluginLoader = pluginLoader;
        }

        public IPluginCollection<ScriptBundle> Plugins
        { 
            get; 
            set; 
        }

        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {            
            Plugins = pluginLoader.LoadPlugins<ScriptBundle>();

            container.Register<IScriptMinifier>((c, p) => DefaultSettings.ScriptMinifier);
            container.Register<IBundlesCache<ScriptBundle>, BundlesCache<ScriptBundle>>();
            container.Register<IBundleConfigurationProvider<ScriptBundle>>((c, p) => DefaultSettings.ScriptConfigurationProvider(c));
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => CreateScriptPipeline(c, Plugins));
            container.Register<ITagWriter<ScriptBundle>, ScriptTagWriter>();
            container.Register<IBundleCachePrimer<ScriptBundle>, ScriptBundleCachePrimer>();
            container.Register<IBundleProvider<ScriptBundle>, ScriptBundleProvider>();
            container.Register<IPluginCollection<ScriptBundle>>(Plugins);
        }

        /// <summary>
        /// Creates the pipeline as well as modifies it using supplied modifiers.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pipelineModifiers"></param>
        /// <returns></returns>
        public IBundlePipeline<ScriptBundle> CreateScriptPipeline(TinyIoCContainer container, IPluginCollection<ScriptBundle> plugins)
        {
            var pipeline = new ScriptPipeline(container);

            plugins.ToList().ForEach(m => m.ModifyPipeline(pipeline));

            return pipeline;
        }

        public void ShutDown()
        {
            Plugins.Dispose();
        }
    }
}
