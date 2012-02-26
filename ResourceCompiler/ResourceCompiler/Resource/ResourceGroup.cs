using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Files;
using System.Collections.ObjectModel;

namespace ResourceCompiler.Resource
{
    public class ResourceGroup
    {
        public ResourceGroup(string name, bool isShared)
        {
            Name = name;
            IsShared = isShared;
            Resources = new List<IResource>();

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

        public IList<IResource> Resources
        {
            get;
            private set;
        }


        private sealed class InternalResourceCollection : Collection<IResource>
        {
            protected override void InsertItem(int index, IResource item)
            {
                if (!AlreadyExists(item))
                {
                    base.InsertItem(index, item);
                }
            }

            protected override void SetItem(int index, IResource item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, "item");
                }

                base.SetItem(index, item);
            }

            private bool AlreadyExists(IResource item)
            {
                return this.Any(i => i != item && i.Source.Equals(item.Source));
            }
        }
    }




}
