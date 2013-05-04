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

    public interface IBundlePipeline<T> : IList<IPipelineProcessor<T>>, IPipelineProcessor<T>
        where T : Bundle
    {
        /// <summary>
        /// Allows adding a processor to be resolved by the container.
        /// </summary>
        /// <typeparam name="TProcessor"></typeparam>
        void Add<TBundleProcessor>()
            where TBundleProcessor : class, IPipelineProcessor<T>;

        /// <summary>
        /// Allows inserting a processor to be resolved by the container.
        /// </summary>
        /// <typeparam name="TProcessor"></typeparam>
        void Insert<TProcessor>(int index)
            where TProcessor : class, IPipelineProcessor<T>;

        /// <summary>
        /// Allows removal of a processor based on its type.
        /// </summary>
        /// <typeparam name="TProcessor"></typeparam>
        void Remove<TProcessor>()
            where TProcessor : class, IPipelineProcessor<T>;

        /// <summary>
        /// Replaces the old process with the new processor at the same position.
        /// </summary>
        /// <typeparam name="OldProcessor"></typeparam>
        /// <typeparam name="NewProcessor"></typeparam>
        void Replace<OldProcessor, NewProcessor>()
            where OldProcessor : class, IPipelineProcessor<T>
            where NewProcessor : class, IPipelineProcessor<T>;

        /// <summary>
        /// Finds the index of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int IndexOf<T>();
    }
}
