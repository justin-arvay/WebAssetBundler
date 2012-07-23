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
    using System.Collections.Generic;
    using WebAssetBundler.Web.Mvc;
    using System.Linq;

    public class WebAssetGroupCollectionResolver : IWebAssetGroupCollectionResolver
    {
        private IWebAssetResolverFactory resolverFactory;

        public WebAssetGroupCollectionResolver(IWebAssetResolverFactory resolverFactory)
        {
            this.resolverFactory = resolverFactory;
        }

        /// <summary>
        /// Resolves all the asset groups into a collection of urls.
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public IList<ResolverResult> Resolve(WebAssetGroupCollection groups, BuilderContext context)
        {
            var results = new List<ResolverResult>();

            foreach (var group in groups)
            {
                //@TODO:: this feels like the wrong place for this, but these need to happen before we resolve
                group.Combine = context.CanCombine(group);
                group.Compress = context.CanCompress(group);

                if (context.DebugMode && context.EnableCacheBreaker)
                {
                    group.Version = context.CreateCacheBreakerVersion();
                }

                var resolver = resolverFactory.Create(group);
                results.AddRange(resolver.Resolve());
            }


            return results;
        }
    }
}
