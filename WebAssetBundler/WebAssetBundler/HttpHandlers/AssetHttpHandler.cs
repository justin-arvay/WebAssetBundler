﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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
    using System.Collections.Generic;

    public class AssetHttpHandler<T> : IWabHttpHandler where T : Bundle
    {
        private IBundleCache<T> cache;

        public AssetHttpHandler(IBundleCache<T> cache)
        {
            this.cache = cache;
        }

        public void ProcessRequest(string path, IResponseWriter writer, IEncoder encoder)
        {
            var bundle = cache.Get(FindName(path));

            if (bundle == null)
            {
                writer.WriteNotFound();                
            }
            else
            {
                if (writer.IsNotModified(bundle))
                {
                    writer.WriteNotModified(bundle);
                }
                else
                {
                    writer.WriteAsset(bundle, encoder);
                }
            }            
        }

        /// <summary>
        /// Finds the name of the resource fro
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string FindName(string path)
        {            
            var parts = new List<string>(path.Split('/'));
            parts.Reverse();
            return parts[0];
        }
    }
}
