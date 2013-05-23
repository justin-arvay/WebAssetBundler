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
    using TinyIoC;

    public class BundlePipeline<T> : List<IPipelineProcessor<T>>, IBundlePipeline<T>
        where T : Bundle
    {
        public BundlePipeline(TinyIoCContainer container, ILogger logger)
        {
            Container = container;
            Logger = logger;
        }

        protected ILogger Logger
        {
            get;
            private set;
        }

        protected TinyIoCContainer Container
        {
            get;
            private set;
        }
        
        public virtual void Process(T bundle)
        {
            bool logEnabled = Logger.IsInfoEnabled;

            if (logEnabled)
            {
                Logger.Info("Start processing bundle: {0}".FormatWith(bundle.Name));
            }

            foreach (var processor in this)
            {
                if (logEnabled)
                {
                    Logger.Info("Executing processor {0}".FormatWith(processor.GetType().AssemblyQualifiedName));
                }

                processor.Process(bundle);
                
            }

            if (logEnabled)
            {
                Logger.Info("End processing bundle: {0}".FormatWith(bundle.Name));
            }
        }

        public void Add<TProcessor>() where TProcessor : class, IPipelineProcessor<T>
        {
            Add(Container.Resolve<TProcessor>());
        }

        public void Insert<TProcessor>(int index) where TProcessor : class, IPipelineProcessor<T>
        {
            Insert(index, Container.Resolve<TProcessor>());
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
