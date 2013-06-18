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
    using System.Drawing;

    public class ImageBundleFactory : IBundleFactory<ImageBundle>
    {
        public ImageBundle Create(AssetBase asset)
        {
            string name = ImageHelper.CreateBundleName(asset);
            string contentType = ImageHelper.GetContentType(asset.Source);
            SizeF dimensions = ImageHelper.GetDimensions(asset.File);

            var bundle = new ImageBundle(contentType);
            bundle.Assets.Add(asset);
            bundle.Name = name;

            //convert to int because SizeF values for bitmap files are in pixels
            bundle.Width = Convert.ToInt32(dimensions.Width);
            bundle.Height = Convert.ToInt32(dimensions.Height);

            return bundle;
        }

        public ImageBundle Create(BundleMetadata metadata)
        {
            throw new NotSupportedException();
        }
    }
}
