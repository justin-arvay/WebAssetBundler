using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public interface IWebAssetContentFilter
    {
        string Filter(string basePath, string content);
    }
}
