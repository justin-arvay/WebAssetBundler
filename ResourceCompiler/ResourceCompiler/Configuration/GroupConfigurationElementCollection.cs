// ResourceCompiler - Compiles web assets so you dont have to.
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
    using System.Configuration;

    [ConfigurationCollection(typeof(AssetConfigurationElement), AddItemName = "asset", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class GroupConfigurationElementCollection : ConfigurationElementCollection
    {
        public GroupConfigurationElementCollection()
        {
            AddElementName = "asset";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AssetConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            return ((AssetConfigurationElement)element).Source;
        }

        public void Add(AssetConfigurationElement element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name 
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }  

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [ConfigurationProperty("version")]
        public string Version
        {
            get
            {
                return (string)this["version"];
            }

            set
            {
                this["version"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetGroupConfigurationElement"/> is compress.
        /// </summary>
        /// <value><c>true</c> if compress; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("compress", DefaultValue = true)]
        public bool Compress
        {
            get
            {
                return (bool)this["compress"];
            }

            set
            {
                this["compress"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetGroupConfigurationElement"/> is combined.
        /// </summary>
        /// <value><c>true</c> if combine; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("combine", DefaultValue = true)]
        public bool Combine
        {
            get
            {
                return (bool)this["combine"];
            }

            set
            {
                this["combine"] = value;
            }
        }
    }
}