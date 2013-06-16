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
    using System.Collections.Generic;

    public class FluentConfiguartionDriver : IConfigurationDriver
    {
        private IFluentConfigurationProvider configProvider;


        public FluentConfiguartionDriver(IFluentConfigurationProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public BundleMetadata LoadMetadata<TBundle>(string name) 
            where TBundle : Bundle
        {
            List<BundleMetadata> metadatas = new List<BundleMetadata>();
            IList<IFluentConfiguration<TBundle>> configs = configProvider.GetConfigurations<TBundle>();
            
            foreach (var config in configs)
            {
                config.Configure();
                metadatas.Add(config.Metadata);
            }

            //Note: probably faster to store as a HashMap with a key of Name+Type

            return metadatas.Find((metadata) => {
                return metadata.Name.Equals(name) &&
                    metadata.Type.Equals(typeof(TBundle));
            });
        }
    }
}
