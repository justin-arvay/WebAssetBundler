﻿// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VersionedWebAssetGroupMerger : IWebAssetMerger
    {
        private IWebAssetReader reader;
        private WebAssetGroup group;

        public VersionedWebAssetGroupMerger(WebAssetGroup group, IWebAssetReader reader)
        {
            this.reader = reader;
            this.group = group;
        }

        public IList<WebAssetMergerResult> Merge()
        {
            var results = new List<WebAssetMergerResult>();

            foreach (var webAsset in group.Assets)
            {
                var content = reader.Read(webAsset);

                results.Add(new WebAssetMergerResult(group.Name, group.Version, content));
            }

            return results;
        }
    }
}
