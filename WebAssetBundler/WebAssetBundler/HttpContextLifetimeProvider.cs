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

    public class HttpContextLifetimeProvider : TinyIoCContainer.ITinyIoCObjectLifetimeProvider
    {
        readonly Func<HttpContextBase> getHttpContext;
        readonly string keyName = String.Format("TinyIoC.HttpContext.{0}", Guid.NewGuid());


        public HttpContextLifetimeProvider(Func<HttpContextBase> getHttpContext)
        {
            this.getHttpContext = getHttpContext;

        }

        public object GetObject()
        {
            return getHttpContext().Items[keyName];
        }

        public void SetObject(object value)
        {
            getHttpContext().Items[keyName] = value;
        }

        public void ReleaseObject()
        {
            var item = GetObject() as IDisposable;
            if (item != null)
            {
                item.Dispose();
            }
            SetObject(null);
        }

            /*

        object TinyIoCContainer.ITinyIoCObjectLifetimeProvider.GetObject()
        {
            return getHttpContext().Items[keyName];
        }

        void TinyIoCContainer.ITinyIoCObjectLifetimeProvider.SetObject(object value)
        {
            getHttpContext().Items[keyName] = value;
        }

        void TinyIoCContainer.ITinyIoCObjectLifetimeProvider.ReleaseObject()
        {
            var item = GetObject() as IDisposable;
            if (item != null)
            {
                item.Dispose();
            }
            SetObject(null);
        }
             * */
    }
}
