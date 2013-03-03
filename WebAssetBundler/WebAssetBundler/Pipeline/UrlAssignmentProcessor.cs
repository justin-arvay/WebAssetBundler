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

    public class UrlAssignmentProcessor<TBundle> : IPipelineProcessor<TBundle>
        where TBundle : Bundle
    {
        private bool debugMode;

        public UrlAssignmentProcessor(Func<bool> debugMode)
        {
            this.debugMode = debugMode();
        }

        public void Process(TBundle bundle)
        {
            var version = bundle.Hash.ToHexString();
            var path = "wab.axd/css/{0}/{1}";

            if (debugMode)
            {
                version = version + DateTime.Now.ToString("MMddyyHmmss");
            }            

            if (bundle.Host.EndsWith("/") == false)
            {
                bundle.Host += "/";
            }

            bundle.Url = bundle.Host + path.FormatWith(version, bundle.Name);
        }
    }
}