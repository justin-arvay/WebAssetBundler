using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Assets;
using System.Web;
using ResourceCompiler.Files;
using ResourceCompiler.Utilities;
using ResourceCompiler.Resolvers;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ResourceCompiler
{
    public class StyleSheetRenderer : IStyleSheetRenderer
    {
        private readonly IStyleSheetAssets _assets;

        //probably change this to a hashtable for better performance
        //static caching for the lifetime of the app. increases performance, only one iteration of reflection
        private IDictionary<string, string> _modelProperties = new Dictionary<string, string>();

        public StyleSheetRenderer(IStyleSheetAssets assets)
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

                if (String.Compare(file.Type, DynamicFileResolver.Type) == 0)
                {
                    ApplyModel(ref styleSheetContent);
                }

                styleSheetContent = StyleSheetPathRewriter.RewriteCssPaths(AppDomain.CurrentDomain.BaseDirectory + "Content", file.Path, styleSheetContent);
                content.Append(styleSheetContent);
            }

            outputContent = content.ToString();
            if (_assets.Compressed)
            {
                outputContent = CompressContent(content.ToString());
            }

            return outputContent;
        }

        public object Model { get; set; }

        private string GetResourceContent(IResource resource)
        {
            FileReader reader = new FileReader(resource.Path);
            return reader.ReadToEnd();
        }

        private string CompressContent(string content)
        {
            return _assets.Compressor.CompressContent(content);
        }

        private void ApplyModel(ref string content)
        {
            if (Model != null)
            {
                if (_modelProperties.Count <= 0)
                {
                    CacheModelProperties();
                }

                //get all words starting with @, ignore case
                // var matches = Regex.Matches(content, @"(\b@)\w+\b", RegexOptions.IgnoreCase);
                //Regex regex = new Regex(@"(\b@)\w+\b", RegexOptions.IgnoreCase);

                foreach (KeyValuePair<string, string> property in _modelProperties)
                {
                    content = content.Replace("@" + property.Key, property.Value);
                }
            }
        }

        private void CacheModelProperties()
        {
            PropertyInfo[] properties = Model.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                _modelProperties.Add(property.Name, property.GetValue(Model, null).ToString());
            }
        }
    }
}
