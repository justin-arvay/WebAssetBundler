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
    using TinyIoC;

    [TaskOrder(4)]
    public class ConfigureScriptsTask : BundleBaseTask<ScriptBundle>
    {
        public ConfigureScriptsTask(IPluginLoader pluginLoader)
        {
            Plugins = pluginLoader.LoadPlugins<ScriptBundle>();
        }

        public override void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {                       
            container.Register<IScriptMinifier>((c, p) => DefaultSettings.ScriptMinifier);
            container.Register<IUrlGenerator<ScriptBundle>, BasicUrlGenerator<ScriptBundle>>();
            container.Register<IBundleCache<ScriptBundle>, BundleCache<ScriptBundle>>();
            container.Register<IBundleFactory<ScriptBundle>, BundleFactory<ScriptBundle>>();
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => CreatePipeline<ScriptPipeline>(c, Plugins));
            container.Register<ITagWriter<ScriptBundle>, ScriptTagWriter>();
            container.Register<IBundleProvider<ScriptBundle>, BundleProvider<ScriptBundle>>();
            container.Register<IPluginCollection<ScriptBundle>>(Plugins);
            container.Register<IBundleRenderer<ScriptBundle>, BundleRenderer<ScriptBundle>>()
                .AsSingleton();
            container.Register<IBundleDependencyResolver<ScriptBundle>, BundleDependencyResolver<ScriptBundle>>()
                .AsSingleton();
        }
    }
}
