using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Files;

namespace ResourceCompiler.Resource
{
    public class ResourceGroupBuilder
    {
        private readonly ResourceGroup group;

        public ResourceGroupBuilder(ResourceGroup group)
        {
            this.group = group;
        }

        public ResourceGroupBuilder Enable(bool value)
        {
            group.Enabled = value;
            return this;
        }

        public ResourceGroupBuilder Version(string value)
        {
            group.Version = value;
            return this;
        }

        public ResourceGroupBuilder Compress(bool value)
        {
            group.Compressed = value;
            return this;
        }

        public ResourceGroupBuilder Combine(bool value)
        {
            group.Combined = value;
            return this;
        }

        public ResourceGroupBuilder Path(string path, Action<PathOnlyBuilder<ResourceGroupBuilder>> builder)
        {
            return this;    
        }

        /// <summary>
        /// Adds a bew resource to the group by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ResourceGroupBuilder Add(string filePath)
        {
            group.Resources.Add(new Files.Resource(filePath));
            return this;
        }

    }
}
