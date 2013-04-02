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

    public class PluginCollection<TBundle> : List<IPlugin<TBundle>>, IPlugin<TBundle>, IPluginCollection<TBundle>
        where TBundle : Bundle
    {
        public void Initialize(TinyIoCContainer container)
        {
            this.ForEach(p => p.Initialize(container));
        }

        public void AddPipelineModifers(ICollection<IPipelineModifier<TBundle>> modifiers)
        {
            foreach (var plugin in this)
            {
                var pluginModifiers = new List<IPipelineModifier<TBundle>>();
                plugin.AddPipelineModifers(modifiers);

                foreach (var pluginModifier in pluginModifiers)
                {
                    modifiers.Add(pluginModifier);
                }
            }
        }

        public void AddSearchPatterns(ICollection<string> patterns)
        {
            this.ForEach(plugin =>
            {
                var innerPatterns = new List<string>();

                plugin.AddSearchPatterns(innerPatterns);
                innerPatterns.ForEach(pattern => patterns.Add(pattern));
            });
        }

        public void Dispose()
        {
            this.ForEach(p => p.Dispose());
        }


        public ICollection<string> GetDirectoryPatterns()
        {
            var patterns = new List<string>();

            AddSearchPatterns(patterns);

            return patterns;
        }

        public ICollection<IPipelineModifier<TBundle>> GetPipelineModifiers()
        {
            var modifers = new List<IPipelineModifier<TBundle>>();

            AddPipelineModifers(modifers);

            return modifers;
        }
    }
}
