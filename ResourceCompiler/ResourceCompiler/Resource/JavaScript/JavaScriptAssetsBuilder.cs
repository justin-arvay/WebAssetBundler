using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Compressors.StyleSheet;
using ResourceCompiler.Resolvers;
using ResourceCompiler.Files;
using System.IO;
using ResourceCompiler.Compressors.JavaScript;
/*
namespace ResourceCompiler.Resource
{
    public class JavaScriptAssetsBuilder : AssetsBuilder<IJavaScriptAssetsBuilder>, IJavaScriptAssetsBuilder
    {
        public JavaScriptAssetsBuilder() : base()
        {
            Compressor = new NullMinifier();
            _files = new List<IResource>();
        }

        public JavaScriptAssetsBuilder(IJavaScriptCompressor compressor)
            : base()
        {
            Compressor = compressor;
        }

        public bool Versioned { get; set; }
        public bool Combined { get; set; }
        public bool Compressed { get; set; }
        public IJavaScriptCompressor Compressor { get; set; }

        public override IJavaScriptAssetsBuilder Add(string path)
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

        public IJavaScriptAssetsBuilder Compress(bool value)
        {
            Compressed = value;
            return this;
        }

        public IJavaScriptAssetsBuilder Combine(bool value)
        {
            Combined = value;
            return this;
        }

        public IJavaScriptAssetsBuilder Version(bool value)
        {
            Versioned = value;
            return this;
        }

        public IJavaScriptAssetsBuilder RendererUrl(string url)
        {
            Route = url;
            return this;
        }

        public IJavaScriptAssetsBuilder SetCompressor(IJavaScriptCompressor compressor)
        {
            Compressor = compressor;
            return this;
        }
    }
}
*/