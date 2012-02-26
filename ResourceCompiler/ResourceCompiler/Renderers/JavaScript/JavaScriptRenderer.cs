using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Resource;
using ResourceCompiler.Files;

namespace ResourceCompiler
{
    public class JavaScriptRenderer : IJavaScriptRenderer
    {
        private readonly IJavaScriptAssetsBuilder _assets;

        public JavaScriptRenderer(IJavaScriptAssetsBuilder assets)
        {
            _assets = assets;
        }
        
        public string Generate()
        {
            StringBuilder content = new StringBuilder();
            string outputContent = String.Empty;

            foreach (var file in _assets.GetFiles())
            {
                string styleSheetContent = GetResourceContent(file);
                content.Append(styleSheetContent);
            }

            outputContent = content.ToString();
            if (_assets.Compressed)
            {
                outputContent = CompressContent(content.ToString());
            }

            return outputContent;
        }

        private string GetResourceContent(IResource resource)
        {
            FileReader reader = new FileReader(resource.Path);
            return reader.ReadToEnd();
        }

        private string CompressContent(string content)
        {
            return _assets.Compressor.CompressContent(content);
        }
    }
}
