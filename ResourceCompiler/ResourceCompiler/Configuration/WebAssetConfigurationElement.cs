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

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(WebAssetConfigurationElement), AddItemName = "asset")]
    public class WebAssetConfigurationElement : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [ConfigurationProperty("source", IsRequired = true, IsKey = true)]
        public string Source
        {
            get
            {
                return (string)this["source"];
            }

            set
            {
                this["source"] = value;
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

        protected override string ElementName
        {
            get
            {
                return "asset";
            }
        }
    }
}
