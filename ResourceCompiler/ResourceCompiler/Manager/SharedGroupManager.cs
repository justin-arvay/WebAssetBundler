using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class SharedGroupManager
    {
        private readonly IList<WebAssetGroup> styleSheets = new List<WebAssetGroup>();
        private readonly IList<WebAssetGroup> scripts = new List<WebAssetGroup>();

        public SharedGroupManager()
        {

        }


    }
}
