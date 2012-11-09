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
    using System.Collections.Generic;

    public class AssetHttpHandler : IWabHttpHandler
    {
        private IMergedBundleCache cache;

        public AssetHttpHandler(IMergedBundleCache cache)
        {
            this.cache = cache;
        }

        public void ProcessRequest(string path, IResponseWriter writer, IEncoder encoder)
        {
            var result = cache.Get(FindName(path));

            if (result == null)
            {
                writer.WriteNotFound();                
            }
            else
            {
                if (writer.IsNotModified(result))
                {
                    writer.WriteNotModified(result.Hash.ToHexString());
                }
                else
                {
                    writer.WriteAsset(result, encoder);
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
