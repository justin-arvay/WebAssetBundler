// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Web;
    using System.Web.Routing;
    using System.Web.Mvc;

    public class UrlResolver : IUrlResolver
    {
        private RequestContext context;

        public UrlResolver(RequestContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns the relative path for the specified virtual path.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string Resolve(string url)
        {
            UrlHelper helper = new UrlHelper(context);

            return helper.Content(url);
        }
    }
}
