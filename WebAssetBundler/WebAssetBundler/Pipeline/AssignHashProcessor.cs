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
    using System.Security.Cryptography;
    using System.IO;
    using System.Linq;

    public class AssignHashProcessor : IPipelineProcessor<Bundle> 
    {
        public void Process(Bundle bundle)
        {
            using (var stream = new MemoryStream())
            {
                bundle.Assets.ForEach((asset) => {
                    asset.Content.CopyTo(stream);

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                    stream.Position = 0;
                    bundle.Hash = md5.ComputeHash(stream);
                });
            }            
        }
    }
}
