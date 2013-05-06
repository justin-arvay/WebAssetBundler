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

    public class BackgroundImageModifier : IAssetModifier
    {
        private IBackgroundImageReader reader;
        private IBundlePipeline<ImageBundle> pipeline;
        private SettingsContext settings;
        private IBundlesCache<ImageBundle> bundlesCache;

        public BackgroundImageModifier(IBackgroundImageReader reader, IBundlePipeline<ImageBundle> pipeline, 
            IBundlesCache<ImageBundle> bundlesCache, SettingsContext settings)
        {
            this.reader = reader;
            this.pipeline = pipeline;
            this.settings = settings;
            this.bundlesCache = bundlesCache;
        }

        public Stream Modify(Stream openStream, AssetBase asset)
        {

        }

        
    }
}
