using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ResourceCompiler.Resolvers
{
    public class DynamicFileResolver: IFileResolver
    {
        public static string Type
        {
            get { return "DynamicFile"; }
        }

        public string Resolve(string file)
        {
            return ResolveAppRelativePathToFileSystem(file);
        }

        protected string ExpandAppRelativePath(string file)
        {
            if (file.StartsWith("~/"))
            {
                string appRelativePath = HttpRuntime.AppDomainAppVirtualPath;
                if (appRelativePath != null && !appRelativePath.EndsWith("/"))
                    appRelativePath += "/";
                return file.Replace("~/", appRelativePath);
            }
            return file;
        }

        protected string ResolveAppRelativePathToFileSystem(string file)
        {
            if (HttpContext.Current == null)
            {
                file = file.Replace("/", "\\").TrimStart('~').TrimStart('\\');
                return @"C:\" + file.Replace("/", "\\");
            }
            return HttpContext.Current.Server.MapPath(file);
        }
    }
}