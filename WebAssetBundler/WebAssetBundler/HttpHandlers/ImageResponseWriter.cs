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
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class ImageResponseWriter : ResponseWriter
    {
        public ImageResponseWriter(HttpContextBase httpContext)
            :base (httpContext)
        {            

        }

        public override void WriteAsset(Bundle bundle, IEncoder encoder)
        {            
            var file = new FileSystemFile(bundle.Assets[0].Source);
            var stream = file.Open(FileMode.Open);

            stream.CopyTo(response.OutputStream);

            //Bitmap bmap = new Bitmap(bundle.Assets[0].Source);

            //response.ContentType = bundle.ContentType;
            //CacheLongTime(bundle.Hash.ToHexString(), bundle.BrowserTtl);

            //bmap.Save(response.OutputStream, ImageFormat.Png);
        }
    }
}
