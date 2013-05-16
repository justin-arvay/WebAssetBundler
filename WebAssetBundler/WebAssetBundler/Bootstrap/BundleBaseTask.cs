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
    using TinyIoC;

    public abstract class BundleBaseTask<TBundle> : IBootstrapTask 
        where TBundle : Bundle
    {
        public IPluginCollection<TBundle> Plugins
        {
            get;
            set;
        }

        /// <summary>
        /// Creates the pipeline as well as modifies it using supplied modifiers.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pipelineModifiers"></param>
        /// <returns></returns>
        public IBundlePipeline<TBundle> CreatePipeline<TPipeline>(TinyIoCContainer container, IPluginCollection<TBundle> plugins)
            where TPipeline : IBundlePipeline<TBundle>
        {
            var pipeline = (TPipeline)container.Resolve(typeof(TPipeline));

            plugins.ToList().ForEach(m => m.ModifyPipeline(pipeline));

            return pipeline;
        }

        public virtual void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {
            throw new NotImplementedException();
        }

        public virtual void ShutDown()
        {
            Plugins.Dispose();
        }
    }
}
