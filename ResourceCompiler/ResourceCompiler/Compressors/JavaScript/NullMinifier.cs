﻿using System.IO;

namespace ResourceCompiler.Compressors.JavaScript
{
    public class NullMinifier: IJavaScriptCompressor
    {
        public static string Identifier
        {
            get { return "null"; }
        }

        string IJavaScriptCompressor.Identifier
        {
            get { return Identifier; }
        }

        public string CompressContent(string content)
        {
            return content;
        }
    }
}