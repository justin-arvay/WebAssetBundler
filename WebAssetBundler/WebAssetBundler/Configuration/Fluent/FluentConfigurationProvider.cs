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

    public class FluentConfigurationProvider: IFluentConfigurationProvider
    {
        private ITypeProvider typeProvider;
        private IFluentConfigurationFactory factory;
        private IAssetProvider assetProvider;
        private IDirectorySearchFactory searchFactory;

        public FluentConfigurationProvider(ITypeProvider typeProvider, IAssetProvider assetProvider,
            IDirectorySearchFactory searchFactory, IFluentConfigurationFactory factory)
        {
            this.typeProvider = typeProvider;
            this.assetProvider = assetProvider;
            this.searchFactory = searchFactory;
            this.factory = factory;
        }

        public IList<IFluentConfiguration<TBundle>> GetConfigurations<TBundle>() 
            where TBundle : Bundle
        {
            var types = typeProvider.GetImplementationTypes(typeof(IFluentConfiguration<TBundle>));
            var configs = new List<IFluentConfiguration<TBundle>>();
            IFluentConfiguration<TBundle> config = null;

            foreach (Type type in types)
            {
                config = factory.Create<TBundle>(type);
                config.AssetProvider = assetProvider;
                config.DirectorySearchFactory = searchFactory;
                config.Metadata = new BundleMetadata()
                {
                    Type = typeof(TBundle)
                };

                configs.Add(config);
            }

            return configs;
        }
    }
}
