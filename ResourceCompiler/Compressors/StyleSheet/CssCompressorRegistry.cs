
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ResourceCompiler.Compressors.StyleSheet
{
    public class CssCompressorRegistry
    {
        private static Dictionary<string, IStyleSheetCompressor> registry = new Dictionary<string, IStyleSheetCompressor>();

        static CssCompressorRegistry()
        {
            var minifierTypes = Assembly.GetAssembly(typeof(MsCompressor)).GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith("Reco.Compressors.StyleSheet"))
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => typeof(IStyleSheetCompressor).IsAssignableFrom(t));

            foreach (Type type in minifierTypes)
            {
                var compressor = (IStyleSheetCompressor)Activator.CreateInstance(type);
                registry.Add(compressor.Identifier, compressor);
            }
        }        

        public static IStyleSheetCompressor Get(string identifier)
        {
            if (registry.ContainsKey(identifier))
            {
                return registry[identifier];    
            }
            return registry[NullCompressor.Identifier];
        }
    }
}
