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

    public interface IPlugin<TBundle> : IDisposable where TBundle : Bundle
    {
        /// <summary>
        /// Initializes the plugin. Best time to configure the container.
        /// </summary>
        /// <param name="container"></param>
        void Initialize(TinyIoCContainer container);

        /// <summary>
        /// Adds registered pipeline modifiers to modify the bundle types pipeline.
        /// </summary>
        /// <param name="modifiers"></param>
        void ModifyPipeline(IBundlePipeline<TBundle> pipeline);

        /// <summary>
        /// Adds new patterns to use when using directory search to add assets to a bundle for bundle type.
        /// </summary>
        /// <param name="patterns"></param>
        void ModifySearchPatterns(ICollection<string> patterns);
    }
}
