﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

    public class BuilderContext
    {

        public BuilderContext()
        {
        }

        public bool Compress
        {
            get;
            set;
        }

        public bool Combine
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public string DefaultPath
        {
            get;
            set;
        }

        public bool DebugMode
        {
            get;
            set;
        }

        public bool EnableCombining
        {
            get;
            set;
        }

        public bool EnableCompressing
        {
            get;
            set;
        }

        public bool EnableCacheBreaker
        {
            get;
            set;
        }

        /// <summary>
        /// Checks if a group can be combined.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool CanCombine(WebAssetGroup group)
        {
            if (group.Combine)
            {
                if (DebugMode && EnableCombining)
                {
                    return true;
                }
                else if (DebugMode == false)
                {
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// Checks if a group can be compressed.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool CanCompress(WebAssetGroup group)
        {
            if (group.Compress)
            {
                if (DebugMode && EnableCompressing)
                {
                    return true;
                }
                else if (DebugMode == false)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates the cache breaker version.
        /// </summary>
        /// <returns></returns>
        public string CreateCacheBreakerVersion()
        {
            return DateTime.Now.ToString("MM_dd_yy_H_mm_ss_fff");
        }

        public IAssetFactory AssetFactory
        {
            get;
            set;
        }
    }
}
