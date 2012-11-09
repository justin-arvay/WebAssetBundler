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
        public WebAssetType Type { get; set; }
        public string Host { get; set; }

        public IList<WebAsset> Assets { get; set; }

        private sealed class InternalCollection : Collection<WebAsset>
        {
            protected override void InsertItem(int index, WebAsset item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(string.Format(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, item.Source), "item");
                }

                base.InsertItem(index, item);
            }

            protected override void SetItem(int index, WebAsset item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(string.Format(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, item.Source), "item");
                }

                base.SetItem(index, item);
            }

            private bool AlreadyExists(WebAsset item)
            {
                return this.Any(i => i != item && i.Source.Equals(item.Source));
            }

            private string Message(WebAsset item)
            {
                return " Asset: \"" + item.Source + "\"";
            }
        }
    }
}
