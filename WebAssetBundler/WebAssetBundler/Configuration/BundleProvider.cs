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
    using System;
    using System.Collections.Generic;

    public class BundleProvider : IBundleProvider
    {
        private BuilderContext context;
        private IConfigurationFactory factory;

        public BundleProvider(BuilderContext context, IConfigurationFactory factory)
        {
            this.context = context;
            this.factory = factory;
        }

        public BundleCollection GetBundles<TBundle>() where TBundle : Bundle
        {
            var bundles = new BundleCollection();

            if ((new StyleSheetBundle()) is TBundle)
            {
                IList<StyleSheetBundleConfiguration> configs = factory.Create<StyleSheetBundleConfiguration, TBundle>(context);                
            
                foreach(StyleSheetBundleConfiguration config in configs)
                {
                    bundles.Add(config.GetBundle());
                }

                return bundles;
            }

            if ((new ScriptBundle()) is TBundle)
            {
                IList<ScriptBundleConfiguration> configs = factory.Create<ScriptBundleConfiguration, TBundle>(context);
            
                foreach(ScriptBundleConfiguration config in configs)
                {
                    bundles.Add(config.GetBundle());
                }

                return bundles;
                
            }

            return new BundleCollection();
        }
    }
}
