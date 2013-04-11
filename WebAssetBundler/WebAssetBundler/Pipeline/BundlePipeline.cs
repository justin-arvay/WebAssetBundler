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

    public class BundlePipeline<T> : List<IPipelineProcessor<T>>, IBundlePipeline<T>
        where T : Bundle
    {
        protected TinyIoCContainer container;

        public BundlePipeline(TinyIoCContainer container)
        {
            this.container = container;
        }
        
        public virtual void Process(T bundle)
        {
            foreach (var processor in this)
            {
                processor.Process(bundle);
            }
        }

        public void Add<TProcessor>() where TProcessor : class, IPipelineProcessor<T>
        {
            Add(container.Resolve<TProcessor>());
        }

        public void Insert<TProcessor>(int index) where TProcessor : class, IPipelineProcessor<T>
        {
            Insert(index, container.Resolve<TProcessor>());
        }

        public void Remove<TProcessor>() where TProcessor : class, IPipelineProcessor<T>
        {
            var type = typeof(TProcessor);            

            for (int index = 0; index < Count - 1; index++)
            {
                if (this[index].GetType().Equals(type))
                {
                    RemoveAt(index);
                    break;
                }
            }
            
        }

        public void Replace<OldProcessor, NewProcessor>()
            where OldProcessor : class, IPipelineProcessor<T>
            where NewProcessor : class, IPipelineProcessor<T>
        {
            var type = typeof(OldProcessor);

            for (int index = 0; index < Count - 1; index++)
            {
                if (this[index].GetType().Equals(type))
                {
                    RemoveAt(index);
                    Insert<NewProcessor>(index);
                    break;
                }
            }
        }

        public int IndexOf<T>()
        {
            var enumerator = this.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
