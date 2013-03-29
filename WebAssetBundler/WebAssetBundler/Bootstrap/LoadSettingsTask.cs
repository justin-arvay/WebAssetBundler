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
    using System.Web.Configuration;

    [TaskOrder(0)]
    public class LoadSettingsTask : IBootstrapTask
    {

        public void StartUp(TinyIoCContainer container, ITypeProvider typeProvider)
        {

            var section = (WebConfigurationManager.GetSection("wab") as WabConfigurationSection)
                   ?? new WabConfigurationSection();

            DefaultSettings.DebugMode = section.DebugMode;
            DefaultSettings.MinifyIdentifier = section.MinifyIdentifier;
        }


        public void ShutDown()
        {

        }
    }
}
