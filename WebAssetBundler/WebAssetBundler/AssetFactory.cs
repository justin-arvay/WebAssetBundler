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

    public class AssetFactory : IAssetFactory
    {
        private BuilderContext context;

        public AssetFactory(BuilderContext context)
        {
            this.context = context;
        }

        public WebAsset CreateAsset(string source)
        {
            if (source.StartsWith("~/") == false)
            {
                source = Path.Combine(context.DefaultPath, source);
            }

            return new WebAsset(source);
        }


        public WebAssetGroup CreateGroup(string name, bool isShared)
        {
            return new WebAssetGroup(name, isShared)
            {
                Combine = context.Combine,
                Compress = context.Compress,                
            };
        }
    }
}
