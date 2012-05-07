using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ResourceCompiler.Web.Mvc
{
    public class MySection : ConfigurationSection
    {
        [ConfigurationProperty("MyCollection", Options = ConfigurationPropertyOptions.IsRequired)]
        public MyCollection MyCollection
        {
            get
            {
                return (MyCollection)this["MyCollection"];
            }
        }
    }

    [ConfigurationCollection(typeof(EntryElement), AddItemName = "entry", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class MyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EntryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((EntryElement)element).Source;
        }

        [ConfigurationProperty("default", IsRequired = false)]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }
        }
    }

    public class EntryElement : ConfigurationElement
    {
        [ConfigurationProperty("source", IsRequired = true, IsKey = true)]
        public string Source
        {
            get
            {
                return (string)base["source"];
            }
        }
    }
}
