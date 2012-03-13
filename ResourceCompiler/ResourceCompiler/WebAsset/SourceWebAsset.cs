
namespace ResourceCompiler.WebAsset
{
    using System.IO;
    using System;

    public class SourceWebAsset : IWebAsset
    {
        public string Source { get; private set; }

        public SourceWebAsset(string path)
        {
            Source = path;
        }

        public bool Exists()
        {
            return File.Exists(Source);
        }

        public DateTime GetLastWrite()
        {
            string fileName = Source;
            DateTime lastWriteDateTime = DateTime.MinValue;
            try
            {
                if (fileName.StartsWith("~"))
                {
                    fileName = fileName.Remove(0);
                    fileName = String.Concat(AppDomain.CurrentDomain.BaseDirectory, fileName);
                }
                lastWriteDateTime = File.GetLastWriteTime(fileName);
            }
            catch { }

            return lastWriteDateTime;
        }
    }
}