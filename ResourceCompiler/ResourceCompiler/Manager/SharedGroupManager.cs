using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.Web.Mvc
{
    public class SharedGroupManager
    {

        public SharedGroupManager()
        {
            StyleSheets = new WebAssetGroupCollection();
            Scripts = new WebAssetGroupCollection();
        }


        public WebAssetGroupCollection StyleSheets
        {
            get;
            set;
        }

        public WebAssetGroupCollection Scripts
        {
            get;
            set;
        }

    }
}
