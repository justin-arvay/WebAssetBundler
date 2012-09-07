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

    public class WebAssetGenerator : IWebAssetGenerator
    {
        private IWebAssetWriter writer;        
        private IMergedResultCache cache;
        private BuilderContext context;

        public WebAssetGenerator(IWebAssetWriter writer, IMergedResultCache cache, BuilderContext context)
        {
            this.writer = writer;            
            this.cache = cache;
            this.context = context;
        }

        public void Generate(IList<WebAssetMergerResult> results)
        {
            foreach (var result in results)
            {
                if (context.DebugMode)
                {
                    writer.Write(result);
                }
                else
                {
                    if (cache.Exists(result) == false)
                    {
                        cache.Add(result);
                        writer.Write(result);
                    }
                }
            }
        }
    }
}
