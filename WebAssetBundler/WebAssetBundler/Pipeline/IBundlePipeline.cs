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
        void Add<TBundleProcessor>()
            where TBundleProcessor : class, IPipelineProcessor<T>;

        void Add<TBundleProcessorFactory>(Func<TBundleProcessorFactory, IPipelineProcessor<T>> create)
            where TBundleProcessorFactory : class;

        void Insert<TBundleProcessor>(int index)
            where TBundleProcessor : class, IPipelineProcessor<T>;

        void Insert<TBundleProcessorFactory>(int index, Func<TBundleProcessorFactory, IPipelineProcessor<T>> create)
            where TBundleProcessorFactory : class;

        void ReplaceWith<TReplacementProcessors>()
            where TReplacementProcessors : class, IEnumerable<IPipelineProcessor<T>>;
    }
}
