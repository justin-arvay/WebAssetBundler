namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    public abstract class Bundle
    {
        public Bundle()
        {
            Enabled = true;
            Assets = new InternalCollection();
        }

        public string Name { get; set; }
        public bool Combine { get; set; }
        public bool Compress { get; set; }
        public bool Enabled { get; set; }
        public WebAssetType Type { get; protected set; }
        public string Host { get; set; }
        public string Extension { get; protected set; }

        public IList<AssetBase> Assets { get; set; }

        private sealed class InternalCollection : Collection<AssetBase>
        {
            protected override void InsertItem(int index, AssetBase item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(string.Format(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, item.Source), "item");
                }

                base.InsertItem(index, item);
            }

            protected override void SetItem(int index, AssetBase item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(string.Format(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, item.Source), "item");
                }

                base.SetItem(index, item);
            }

            private bool AlreadyExists(AssetBase item)
            {
                return this.Any(i => i != item && i.Source.Equals(item.Source));
            }

            private string Message(AssetBase item)
            {
                return " Asset: \"" + item.Source + "\"";
            }
        }
    }
}
