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
    using System.Configuration;

    public class GroupConfigurationElement : ConfigurationElementCollection
    {
        public GroupConfigurationElement()
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }

            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetGroupConfigurationElement"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }

            set
            {
                this["enabled"] = value;
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
        /// <value><c>true</c> if combined; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("combined", DefaultValue = true)]
        public bool Combined
        {
            get
            {
                return (bool)this["combined"];
            }

            set
            {
                this["combined"] = value;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new WebAssetConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebAssetConfigurationElement)element).Source;
        }
    }
}
