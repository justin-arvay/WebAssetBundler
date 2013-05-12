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

    public class ImageBundleFactory : IBundleFactory<ImageBundle>
    {
        private IAssetProvider assetProvider;

        public ImageBundleFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public ImageBundle CreateFromSource(string source)
        {
            string name = ImageHelper.CreateBundleName(source);
            string contentType = ImageHelper.GetContentType(source);

            AssetBase asset = assetProvider.GetAsset(source);

            var bundle = new ImageBundle(contentType);
            bundle.Assets.Add(asset);
            bundle.Name = name;
            bundle.Width = 0;
            bundle.Height = 0;

            return bundle;
        }
    }
}
