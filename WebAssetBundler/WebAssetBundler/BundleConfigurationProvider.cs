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
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq;

    public class BundleConfigurationProvider<TBundle> : IBundleConfigurationProvider<TBundle>
        where TBundle : Bundle
    {
        private IBundleConfigurationFactory<TBundle> factory;
        private ITypeProvider typeProvider;

        public BundleConfigurationProvider(IBundleConfigurationFactory<TBundle> factory, ITypeProvider typeProvider)
        {
            this.factory = factory;
            this.typeProvider = typeProvider;
        }

        public IList<IBundleConfiguration<TBundle>> GetConfigs()
        {
            var types = typeProvider.GetImplementationTypes(typeof(IBundleConfiguration<TBundle>));
            var configs = new List<IBundleConfiguration<TBundle>>();

            foreach (Type type in types)
            {
                configs.Add(factory.Create(type));
            }

            return configs;
        }
    }
}
