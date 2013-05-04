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
    using System.IO;

    public class ImageBundle : Bundle
    {
        private string contentType;

        public ImageBundle(string contentType, string url)
        {
            this.contentType = contentType;
            Url = url;

            //TODO:: need unique name. use the image file name without .ext, but instead -ext then hash path
            // example: asdfsas123sasaf-nugetlogo-png
            Name = GetName(url);
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public string GetName(string url)
        {
            var directoryName = Path.GetDirectoryName(url);
            var fileName = Path.GetFileName(url);

            return directoryName.ToHash() + "-" + fileName.Replace('.', '-');
        }

        public override string AssetSeparator
        {
            get 
            { 
                return String.Empty; 
            }
        }
    }
}
