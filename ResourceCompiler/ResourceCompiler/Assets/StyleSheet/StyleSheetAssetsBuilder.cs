using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Compressors.StyleSheet;
using System.Web;
using System.IO;
using ResourceCompiler.Resolvers;
using ResourceCompiler.Files;
using ResourceCompiler.Assets;

namespace ResourceCompiler.Assets
{


    public class StyleSheetAssetsBuilder : Assets, IStyleSheetAssetsBuilder
    {
        
        public StyleSheetAssetsBuilder() : base()
        {
            MediaType = "screen";
            Compressor = new NullCompressor();
            _files = new List<IResource>();
        }

        public StyleSheetAssetsBuilder(IStyleSheetCompressor compressor)
            : base()
        {
            MediaType = "screen";
            Compressor = compressor;
        }

        public bool Versioned { get; set; }
        public bool Combined { get; set; }
        public bool Compressed { get; set; }
        public string MediaType { get; set; }
        public IStyleSheetCompressor Compressor { get; set; }

        public IStyleSheetAssetsBuilder Add(string path)
        {
            FileResolver resolver = new FileResolver();
            IResource file = new Resource(resolver.Resolve(path), FileResolver.Type);

            if (FileExists(file) == false)
            {
                if (file.Exists())
                {
                    AddResource(file);
                }
                else
                {
                    throw new FileNotFoundException(string.Format("File \"{0}\" could not be found.", path));
                }
            }

            return this;
        }

        public IStyleSheetAssetsBuilder AddDynamic(string path)
        {
            DynamicFileResolver resolver = new DynamicFileResolver();
            IResource file = new Resource(resolver.Resolve(path), DynamicFileResolver.Type);

            if (FileExists(file) == false)
            {
                if (file.Exists())
                {
                    AddResource(file);
                }
                else
                {
                    throw new FileNotFoundException(string.Format("File \"{0}\" could not be found.", path));
                }
            }

            return this;
        }

        public IStyleSheetAssetsBuilder Compress(bool value)
        {
            Compressed = value;
            return this;
        }

        public IStyleSheetAssetsBuilder Combine(bool value)
        {
            Combined = value;
            return this;
        }

        public IStyleSheetAssetsBuilder Version(bool value)
        {
            Versioned = value;
            return this;
        }

        public IStyleSheetAssetsBuilder Media(string value)
        {
            MediaType = value;
            return this;
        }

        public IStyleSheetAssetsBuilder RendererUrl(string url)
        {
            Route = url;
            return this;
        }

        public IStyleSheetAssetsBuilder SetCompressor(IStyleSheetCompressor compressor)
        {
            Compressor = compressor;
            return this;
        }

    }
}
