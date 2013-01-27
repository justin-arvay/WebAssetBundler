namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    public abstract class Bundle
    {
        public Bundle()
        {
            Minify = true;
            Assets = new InternalCollection();
            Content = "";
        }

        public string Name { get; set; }
        public bool Minify { get; set; }
        public WebAssetType Type { get; protected set; }
        public string Host { get; set; }
        public string Extension { get; protected set; }
        public abstract string ContentType { get; }

        public IList<AssetBase> Assets { get; set; }

        public string Content
        {
            get;
            set;
        }

        public bool IsExternal
        {
            get
            {
                return Assets.Count == 1 && Assets[0] is ExternalAsset;
            }
        }
        
        public byte[] Hash
        {
            get
            {
                MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
                byte[] bs = Encoding.UTF8.GetBytes(Content);
                return x.ComputeHash(bs);
            }
        }

        public void TransformAssets(IAssetTransformer transformer)
        {
            ((InternalCollection)Assets).Transform(transformer);
        }

        private sealed class InternalCollection : Collection<AssetBase>, ITransformable<IAssetTransformer>
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
                return this.Any(i => i.Source.Equals(item.Source));
            }

            private string Message(AssetBase item)
            {
                return " Asset: \"" + item.Source + "\"";
            }

            public void Transform(IAssetTransformer transformer)
            {
                foreach (var asset in this)
                {
                    asset.Transform(transformer);
                }
            }
        }
    }
}
