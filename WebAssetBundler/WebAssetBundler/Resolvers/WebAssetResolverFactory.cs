// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Linq;
    using WebAssetBundler.Web.Mvc;

    public class WebAssetResolverFactory : IWebAssetResolverFactory
    {                

        public IWebAssetResolver Create(WebAssetGroup webAssetGroup)
        {
            if (webAssetGroup.Combine)
            {
                return new CombinedWebAssetGroupResolver(webAssetGroup);
            }

            if (webAssetGroup.Version.IsNotNullOrEmpty())
            {
                return new VersionedWebAssetGroupResolver(webAssetGroup);
            }

            return new WebAssetGroupResolver(webAssetGroup);
        }
    }
}
