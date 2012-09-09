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
        private BuilderContext context;

        public WebAssetResolverFactory(BuilderContext context)
        {
            this.context = context;
        }

        public IWebAssetResolver Create(WebAssetGroup group)
        {
            if (group.Enabled == false)
            {
                return new DoNothingWebAssetResolver();
            }

            if (group.Combine)
            {
                if (context.DebugMode && context.EnableCombining)
                {
                    return new CombinedWebAssetGroupResolver(group);
                }
                else if (context.DebugMode == false)
                {
                    return new CombinedWebAssetGroupResolver(group);
                }
            }

            if (group.Version.IsNotNullOrEmpty())
            {
                return new VersionedWebAssetGroupResolver(group);
            }

            return new WebAssetGroupResolver(group);
        }
    }
}
