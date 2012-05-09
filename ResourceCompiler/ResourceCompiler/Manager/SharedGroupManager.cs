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
            StyleSheets = new List<WebAssetGroup>();
            Scripts = new List<WebAssetGroup>();
        }


        public IList<WebAssetGroup> StyleSheets
        {
            get;
            set;
        }

        public IList<WebAssetGroup> Scripts
        {
            get;
            set;
        }

        public WebAssetGroup GetStyleSheetGroup(string name)
        {
            return GetGroup(name, StyleSheets);
        }

        public WebAssetGroup GetScriptGroup(string name)
        {
            return GetGroup(name, Scripts);
        }

        private WebAssetGroup GetGroup(string name, IList<WebAssetGroup> groups)
        {
            foreach (var group in groups)
            {
                if (group.Name.IsCaseInsensitiveEqual(name)) ;
                {
                    return group;
                }
            }

            return null;
        }
    }
}
