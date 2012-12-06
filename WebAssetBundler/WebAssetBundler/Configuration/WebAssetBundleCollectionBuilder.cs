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
    using System;

    public class WebAssetBundleCollectionBuilder<TBundle> where TBundle : Bundle
    {
        private BundleCollection<TBundle> bundles;
        private BuilderContext context;

        public WebAssetBundleCollectionBuilder(BundleCollection<TBundle> bundles, BuilderContext context)
        {            
            this.bundles = bundles;            
            this.context = context;
        }

        /// <summary>
        /// Adds a file to the collection.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public WebAssetBundleCollectionBuilder<TBundle> Add(string source)
        {
            var asset = context.AssetFactory.CreateAsset(source, "");
            var bundle = context.AssetFactory.CreateBundle<TBundle>(asset.Name);

            bundle.Assets.Add(asset);

            //add to collection
            bundles.Add(bundle);

            return this;
        }
    }
}