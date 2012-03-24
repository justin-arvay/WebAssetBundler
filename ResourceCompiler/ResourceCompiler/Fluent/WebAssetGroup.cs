using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ResourceCompiler.Web.Mvc
{
    public class WebAssetGroup
    {
        public WebAssetGroup(string name, bool isShared)
        {
            Name = name;
            IsShared = isShared;
            Assets = new InternalCollection();

        }

        public string Name
        {
            get;
            private set;
        }

        public bool IsShared
        {
            get;
            private set;
        }

        public string DefaultPath
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        public bool Compressed
        {
            get;
            set;
        }

        public bool Combined
        {
            get;
            set;
        }

        public IList<IWebAsset> Assets
        {
            get;
            private set;
        }


        private sealed class InternalCollection : Collection<IWebAsset>
        {
            protected override void InsertItem(int index, IWebAsset item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, "item");
                }

                base.InsertItem(index, item);
            }

            protected override void SetItem(int index, IWebAsset item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, "item");
                }

                base.SetItem(index, item);
            }

            private bool AlreadyExists(IWebAsset item)
            {
                return this.Any(i => i != item && i.Source.Equals(item.Source));
            }
        }
    }




}
