using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using ResourceCompiler.Compressors.StyleSheet;
using ResourceCompiler.Files;
using System.Web.Routing;

namespace ResourceCompiler.Assets
{
    public abstract class Assets
    {
        protected const string _versionFormat = "MMddyyyyHHmmss";
        protected IList<IResource> _files;

        public Assets()
        {
            _files = new List<IResource>();
        }

        public void AddResource(IResource file)
        {
            _files.Add(file);
        }

        public string Route { get; set; }

        public virtual IList<IResource> GetFiles()
        {
            return _files;
        }

        public string GetLastWriteTimestamp()
        {
            DateTime dateTime = DateTime.MinValue;
            foreach (IResource file in GetFiles())
            {
                DateTime fileDateTime = file.GetLastWrite();
                if (fileDateTime > dateTime)
                {
                    dateTime = fileDateTime;
                }
            }

            return dateTime.ToString(_versionFormat);
        }

        protected bool FileExists(IResource inputFile)
        {
            return _files.Any<IResource>(i => i != inputFile && i.Path.Equals(inputFile.Path));
        }
    }    
}
