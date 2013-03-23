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

    [TaskOrder(3)]
    public class ConfigureScriptsTask : ConfigureContainerTaskBase
    {

        public override void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var settings = CreateSettings<ScriptBundle>();
            var plugins = LoadPlugins<ScriptBundle>(container, typeProvider);

            foreach (var plugin in plugins)
            {
                plugin.Configure(container);
                plugin.ConfigurePatternModifiers(settings.PiplineModifiers);
            }

            ConfigureContainer(container, typeProvider);
        }

        public void ConfigureContainer(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            container.Register<SettingsContext<ScriptBundle>>((c, p) => CreateSettings<ScriptBundle>());
            container.Register<IAssetProvider<ScriptBundle>, AssetProvider<ScriptBundle>>();
            container.Register<IScriptMinifier>((c, p) => DefaultSettings.ScriptMinifier);
            container.Register<IBundlesCache<ScriptBundle>, BundlesCache<ScriptBundle>>();
            container.Register<IBundleConfigurationProvider<ScriptBundle>>((c, p) => DefaultSettings.ScriptConfigurationProvider(c));
            container.Register<IBundlePipeline<ScriptBundle>>((c, p) => CreateScriptPipeline(c, typeProvider));
            container.Register<ITagWriter<ScriptBundle>, ScriptTagWriter>();
            container.Register<IBundleCachePrimer<ScriptBundle>, ScriptBundleCachePrimer>();
            container.Register<IBundleProvider<ScriptBundle>, ScriptBundleProvider>();
        }

        public IBundlePipeline<ScriptBundle> CreateScriptPipeline(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var pipeline = new ScriptPipeline(container);

            container.RegisterMultiple<IPipelineModifier<ScriptBundle>>(typeProvider.GetImplementationTypes(typeof(IPipelineModifier<ScriptBundle>)));

            foreach (var customizer in container.ResolveAll<IPipelineModifier<ScriptBundle>>())
            {
                customizer.Customize(pipeline);
            }

            return pipeline;
        }
    }
}
