
namespace ResourceCompiler.Files
{
    using System.IO;
    using System;

    public class Resource : IResource
    {
        public string Path { get; private set; }

        public Resource(string path)
        {
            Path = path;
        }

        public bool Exists()
        {
            return File.Exists(Path);
        }

        public DateTime GetLastWrite()
        {
            string fileName = Path;
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