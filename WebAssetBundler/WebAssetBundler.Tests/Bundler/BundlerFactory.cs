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
    using System.Web;
    using TinyIoC;

    public class BundlerFactory
    {
        private HttpContextBase context;
        private TinyIoCContainer container;

        public BundlerFactory(HttpContextBase context, TinyIoCContainer container)
        {
            this.context = context;
            this.container = container;
        }

        public T Create<T, TBundle>()
            where T : BundlerBase<TBundle>
            where TBundle : Bundle
        {
            T bundler = container.Resolve<T>();
            bundler.State = GetBundlerState(bundler.GetType().Name);
            
            return bundler;
        }

        private BundlerState GetBundlerState(string name)
        {
            var obj = (BundlerState)context.Items[name];

            if (obj == null)
            {
                obj = new BundlerState();
                context.Items[name] = obj;
            }

            return obj;
        }
    }
}
