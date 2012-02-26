using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using ResourceCompiler.Compressors.StyleSheet;
using ResourceCompiler.Files;
using System.Web.Routing;
/*
namespace ResourceCompiler.Resource
{
    public abstract class AssetsBuilder<TBuilder>
        where TBuilder : IAssetsBuilder<TBuilder>
    {
        protected const string versionFormat = "MMddyyyyHHmmss";
        protected IList<IResource> _files;

        public AssetsBuilder()
        {
            _files = new List<IResource>();
        }

        protected void AddResource(IResource file)
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

            return dateTime.ToString(versionFormat);
        }

        protected bool FileExists(IResource inputFile)
        {
            return _files.Any<IResource>(i => i != inputFile && i.Path.Equals(inputFile.Path));
        }

        public abstract TBuilder Add(string path);

        public TBuilder Path(string path, Action<PathOnlyBuilder<TBuilder>> action)
        {
            if (System.IO.Path.IsPathRooted(path))
            {
                throw new ArgumentException("Path must be a relative path.");
            }

            action(new PathOnlyBuilder<TBuilder>(path, this as TBuilder));
            return this as TBuilder;
        }

    }    
}
*/