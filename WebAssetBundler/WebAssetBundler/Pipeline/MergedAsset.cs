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
    using System.Collections.Generic;
using System.IO;

    public class MergedAsset : AssetBase
    {
        private Stream stream;

        public MergedAsset(AssetCollection assets)            
        {
            stream = MergeAssetsIntoSingleStream(assets);
        }

        public override string Source
        {
            get { throw new NotSupportedException("Asset is not a real file."); }
        }

        Stream MergeAssetsIntoSingleStream(AssetCollection assets)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            assets.ForEach(a => WriteAsset(a, writer));

            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public void WriteAsset(AssetBase asset, StreamWriter writer)
        {
            using (var reader = new StreamReader(asset.Content))
            {
                var content = reader.ReadToEnd();
                writer.Write(content);
            }
        }

        protected override Stream OpenSourceStream()
        {
            return stream;
        }
    }
}
