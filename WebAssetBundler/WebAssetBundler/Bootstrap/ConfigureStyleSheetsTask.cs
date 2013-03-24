﻿// ResourceCompiler - Compiles web assets so you dont have to.
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

    [TaskOrder(2)]
    public class ConfigureStyleSheetsTask : ConfigureContainerTaskBase
    {

        public override void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var settings = CreateSettings<StyleSheetBundle>();
            var plugins = LoadPlugins<StyleSheetBundle>(container, typeProvider);

            foreach (var plugin in plugins)
            {
                plugin.Initialize(container);
                plugin.Configure(settings);
            }

            ConfigureContainer(container, typeProvider, settings);
        }

        public void ConfigureContainer(TinyIoCContainer container, ITypeProvider typeProvider, SettingsContext<StyleSheetBundle> settings)
        {
            container.Register<SettingsContext<StyleSheetBundle>>(settings);
            container.Register<IAssetProvider<StyleSheetBundle>, AssetProvider<StyleSheetBundle>>();
            container.Register<IStyleSheetMinifier>((c, p) => DefaultSettings.StyleSheetMinifier);
            container.Register<IBundlesCache<StyleSheetBundle>, BundlesCache<StyleSheetBundle>>();
            container.Register<IBundleConfigurationProvider<StyleSheetBundle>>((c, p) => DefaultSettings.StyleSheetConfigurationProvider(c));
            container.Register<IBundlePipeline<StyleSheetBundle>>((c, p) => CreateStyleSheetPipeline(c, typeProvider));
            container.Register<ITagWriter<StyleSheetBundle>, StyleSheetTagWriter>();
            container.Register<IBundleProvider<StyleSheetBundle>, StyleSheetBundleProvider>();
            container.Register<IBundleCachePrimer<StyleSheetBundle>, StyleSheetBundleCachePrimer>();
            container.Register<IBundleProvider<StyleSheetBundle>, StyleSheetBundleProvider>();
        }

        public IBundlePipeline<StyleSheetBundle> CreateStyleSheetPipeline(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            var pipeline = new StyleSheetPipeline(container);

            container.RegisterMultiple<IPipelineModifier<StyleSheetBundle>>(typeProvider.GetImplementationTypes(typeof(IPipelineModifier<StyleSheetBundle>)));

            foreach (var customizer in container.ResolveAll<IPipelineModifier<StyleSheetBundle>>())
            {
                customizer.Customize(pipeline);
            }

            return pipeline;
        }
    }
}
