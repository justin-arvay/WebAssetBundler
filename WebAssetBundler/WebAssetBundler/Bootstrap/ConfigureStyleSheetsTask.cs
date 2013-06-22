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

    [TaskOrder(3)]
    public class ConfigureStyleSheetsTask : BundleBaseTask<StyleSheetBundle>
    {
        public ConfigureStyleSheetsTask(IPluginLoader pluginLoader)
        {
            Plugins = pluginLoader.LoadPlugins<StyleSheetBundle>();
        }

        public override void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {           
            container.Register<IStyleSheetMinifier>((c, p) => DefaultSettings.StyleSheetMinifier);
            container.Register<IUrlGenerator<StyleSheetBundle>, BasicUrlGenerator<StyleSheetBundle>>();
            container.Register<IBundleCache<StyleSheetBundle>, BundleCache<StyleSheetBundle>>();
            container.Register<IBundleFactory<StyleSheetBundle>, BundleFactory<StyleSheetBundle>>();
            container.Register<IBundleCache<ImageBundle>, BundleCache<ImageBundle>>();
            container.Register<IUrlGenerator<ImageBundle>, ImageUrlGenerator>();
            container.Register<ITagWriter<StyleSheetBundle>, StyleSheetTagWriter>();
            container.Register<IBundleProvider<StyleSheetBundle>, BundleProvider<StyleSheetBundle>>();
            container.Register<IPluginCollection<StyleSheetBundle>>(Plugins);
            container.Register<IImagePipelineRunner, ImagePipelineRunner>();
            container.Register<IBundlePipeline<StyleSheetBundle>>((c, p) => CreatePipeline<StyleSheetPipeline>(c, Plugins));
            container.Register<IBundleRenderer<StyleSheetBundle>, BundleRenderer<StyleSheetBundle>>()
                .AsSingleton();
            container.Register<IBundleDependencyResolver<StyleSheetBundle>, BundleDependencyResolver<StyleSheetBundle>>()
                .AsSingleton();
        }        
    }
}
