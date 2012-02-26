using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Files;

namespace ResourceCompiler.Resource
{
    public class ResourceGroup
    {
        public ResourceGroup(string name, bool isShared)
        {
            Name = name;
            IsShared = isShared;
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
    }
}
