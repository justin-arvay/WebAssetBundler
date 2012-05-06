

namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;

    [ConfigurationCollection(typeof(GroupConfigurationElementCollection), AddItemName="group")]
    public class StyleSheetConfigurationElementCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupConfigurationElementCollection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            return ((GroupConfigurationElementCollection)element).Name;
        }
    }
}
