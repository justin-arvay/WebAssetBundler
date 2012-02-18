
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ResourceCompiler.Assets;

namespace ResourceCompiler
{
    public class RecoAssets
    {
        //singleton instantiation to avoid using lock
        private static IStyleSheetAssets _styleSheetRegistrar = new StyleSheetAssets();
        private static IJavaScriptAssets _jsRegistrar = new JavaScriptAssets();

        public static IStyleSheetAssets StyleSheet()
        {
            return _styleSheetRegistrar;
        }

        public static IJavaScriptAssets JavaScript()
        {
            return _jsRegistrar;
        }

        /*
        public static string CurrentVersion()
        {
            try
            {
                return ConfigurationManager.AppSettings["version"];
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }*/

    }
}
